using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MiniShop.Persistence.Entities
{
    public class Product : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Likes { get; set; }
        public bool IsTopRate { get; set; }
        public bool IsApproved { get; set; }

        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
    }

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("TblProduct");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreateDate).IsRequired(true);
            builder.Property(x => x.Title).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.Description).IsRequired(true);
            builder.Property(x => x.Likes).IsRequired(true);
            builder.Property(x => x.IsTopRate).IsRequired(true);
            builder.Property(x => x.ProductCategoryId).IsRequired(true);
            builder.Property(x => x.IsApproved).IsRequired(true);

            builder.HasOne(x => x.ProductCategory).WithMany(x => x.Products).HasForeignKey(x => x.ProductCategoryId);
        }
    }
}
