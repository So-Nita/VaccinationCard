using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vaccination.EnityModel;

namespace Vaccination.EnityConfiguration
{
    public class ProvinceEntity : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.HasKey(e=>e.Id);
            builder.HasIndex(e => e.ProvinceName);

            builder.Property(e => e.Id).IsRequired().HasColumnType("varchar").HasMaxLength(36);
            builder.Property(e => e.ProvinceName).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(e => e.Deleted).IsRequired(false).HasColumnType("bit");
        }
    }
}
