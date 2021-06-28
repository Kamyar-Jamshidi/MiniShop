using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MiniShop.Persistence.Entities;

namespace MiniShop.Persistence
{
    public class MiniShopDB : DbContext
    {
        public MiniShopDB(DbContextOptions options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new AdminConfiguration());
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }

    }

    public class ShopingContextFactory : IDesignTimeDbContextFactory<MiniShopDB>
    {
        public MiniShopDB CreateDbContext(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder<MiniShopDB>();
            optionBuilder.UseSqlServer(@"Server=.;Database=MiniShopDB;Trusted_Connection=True");

            return new MiniShopDB(optionBuilder.Options);
        }
    }
}
