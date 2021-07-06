using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniShop.Core.DTO;
using MiniShop.Persistence.Entities;
using System;

namespace MiniShop.Persistence
{
    public class MiniShopDB : IdentityDbContext<User>
    {
        public MiniShopDB(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory() { Id = 1, CreateDate = DateTime.Now, Title = "Mobile" },
                new ProductCategory() { Id = 2, CreateDate = DateTime.Now, Title = "Labtop" }
                );

            base.OnModelCreating(modelBuilder);
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync(Role.Admin).Result)
                _ = roleManager.CreateAsync(new IdentityRole(Role.Admin)).Result;

            if (!roleManager.RoleExistsAsync(Role.Operator).Result)
                _ = roleManager.CreateAsync(new IdentityRole(Role.Operator)).Result;
        }
        public static void SeedUsers(UserManager<User> userManager)
        {
            if (userManager.FindByNameAsync("Admin").Result == null)
            {
                var user = new User
                {
                    CreateDate = DateTime.Now,
                    EmailConfirmed = true,
                    FirstName = "Admin",
                    LastName = "Admin",
                    IsApproved = true,
                    UserName = "admin",
                };

                var result = userManager.CreateAsync(user, "admin").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Role.Admin).Wait();
                }
            }
        }

        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
