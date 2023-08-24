using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vaccination.EnityModel;

namespace Vaccination.EnityConfiguration
{
    public class CustomerEntity : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.Property(e=>e.Id).IsRequired().HasColumnType("varchar").HasMaxLength(36);
            builder.Property(e => e.Name).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(e => e.DOB).IsRequired().HasColumnType("date");
            builder.Property(e => e.IdentityId).IsRequired().HasColumnType("int").HasMaxLength(10);
            builder.Property(e => e.ProvinceId).IsRequired().HasColumnType("varchar").HasMaxLength(36);
            builder.HasOne(e => e.Province)
                .WithMany() // No navigation property reference
                .HasForeignKey(e => e.ProvinceId)
                .IsRequired();
        }
    }
}
