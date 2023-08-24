using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vaccination_WebApi.EnityModel;

namespace Vaccination_WebApi.EnityConfiguration
{
    public class VaccinatedRecordEntity : IEntityTypeConfiguration<VaccinatedRecord>
    {
        public void Configure(EntityTypeBuilder<VaccinatedRecord> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).IsRequired().HasColumnType("varchar").HasMaxLength(36);
            builder.Property(e => e.Customer_Id).IsRequired().HasColumnType("varchar").HasMaxLength(36);
            builder.Property(e => e.Card_Id).IsRequired().HasColumnType("varchar").HasMaxLength(36);
            builder.Property(e => e.DoeseReceived).HasColumnType("int");
            builder.Property(e => e.DateVaccinated).HasColumnType("date");

            builder.HasOne(e => e.Customer).WithMany(c => c.VaccinatedRecords).HasForeignKey(e => e.Customer_Id).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
