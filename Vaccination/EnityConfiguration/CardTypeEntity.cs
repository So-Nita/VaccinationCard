using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vaccination_WebApi.EnityModel;

namespace Vaccination_WebApi.EnityConfiguration
{
    public class CardTypeEntity : IEntityTypeConfiguration<CardType>
    {
        public void Configure(EntityTypeBuilder<CardType> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.CardName);

            builder.Property(e => e.Id).IsRequired().HasColumnType("varchar").HasMaxLength(36);
            builder.Property(e => e.CardName).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(e => e.Create).HasColumnType("date");
            builder.Property(e => e.Deleted).IsRequired(false).HasColumnType("bit");
        }
    }
}
