using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Vaccination.EnityModel;

namespace Vaccination.EnityConfiguration
{
    public class VaccinationContext : DbContext
    {
        public VaccinationContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Province> Provinces { get; set; } 
        public DbSet<CardType> CardTypes { get; set; } 
        public DbSet<VaccinatedRecord> VaccinatedRecords { get; set; }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
