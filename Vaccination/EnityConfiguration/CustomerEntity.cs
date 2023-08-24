using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vaccination_WebApi.EnityModel;

namespace Vaccination_WebApi.EnityConfiguration
{
    public class CustomerEntity : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.Property(e=>e.Id).IsRequired().HasColumnType("varchar").HasMaxLength(36);
            builder.Property(e => e.Name).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(e => e.DOB).IsRequired().HasColumnType("date");
            builder.Property(e=>e.ProvinceId).IsRequired().HasColumnType("varchar").HasMaxLength(36);
            builder.Property(e => e.IdentityId).IsRequired().HasColumnType("int").HasMaxLength(10);
            builder.HasOne(e => e.Province)
                .WithOne() // Remove the navigation property reference (e => e.Id)
                .HasForeignKey<Customer>(e => e.ProvinceId) // Define the foreign key
                .IsRequired();
        }
    }
}
