using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MiniShop.Persistence.Entities
{
    public class Admin : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsSuperAdmin { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsApproved { get; set; }
        public string Token { get; set; }
    }

    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.ToTable("TblAdmin");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreateDate).IsRequired(true);
            builder.Property(x => x.FirstName).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.LastName).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.IsSuperAdmin).IsRequired(true);
            builder.Property(x => x.Username).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.Password).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.IsApproved).IsRequired(true);
            builder.Property(x => x.Token).HasMaxLength(100).IsRequired(true);

        }
    }
}
