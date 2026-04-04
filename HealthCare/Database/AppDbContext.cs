using HealthCare.Entities;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.Family).IsRequired();
                e.Property(x => x.GivenName).IsRequired();

                e.HasIndex(x => x.BirthDate);
            });
        }
    }
}
