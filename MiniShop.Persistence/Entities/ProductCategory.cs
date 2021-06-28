using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace MiniShop.Persistence.Entities
{
    public class ProductCategory : BaseEntity
    {
        public string Title { get; set; }

        public ICollection<Product> Products { get; set; }
    }

    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("TblProductCategory");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreateDate).IsRequired(true);
            builder.Property(x => x.Title).HasMaxLength(100).IsRequired(true);
        }
    }
}
