using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MiniShop.Api.Model.Authentication;
using MiniShop.Api.Model.Authentication.Interfaces;
using MiniShop.Core.DTO;
using MiniShop.Core.Interfaces.Repositories;
using MiniShop.Persistence;
using MiniShop.Persistence.Entities;
using MiniShop.Persistence.Repositories;

namespace MiniShop.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MiniShopDB>(option =>
                option.UseSqlServer(Configuration.GetConnectionString("MiniShopDB")));

            #region Identity

            services.AddIdentity<User, IdentityRole>(option =>
            {
                option.SignIn.RequireConfirmedAccount = false;
                option.Password.RequireDigit = false;
                option.Password.RequiredLength = 3;
                option.Password.RequireLowercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;
                option.Password.RequiredUniqueChars = 0;
            }).AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<MiniShopDB>();

            #endregion

            #region AddAuthentication

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Basic";
                options.DefaultScheme = "Basic";
            }).AddScheme<BasicAuthenticationOptions, AuthenticationHandler>("Basic", null);

            services.AddSingleton<IAuthenticationManager, AuthenticationManager>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Role.PowerUser,
                    policy => policy.RequireRole(Role.Admin));
            });

            #endregion

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/api/Account/AccessDenied";
                options.AccessDeniedPath = $"/api/Account/AccessDenied";
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            #region Cors

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .SetIsOriginAllowed((host) => true)
                        .AllowAnyHeader());
            });

            #endregion

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            app.UseCors("CorsPolicy");

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<MiniShopDB>();
                context.Database.EnsureCreated();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            MiniShopDB.SeedRoles(roleManager);
            MiniShopDB.SeedUsers(userManager);
        }
    }
}
