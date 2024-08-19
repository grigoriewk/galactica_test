using galactica_test.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace galactica_test.Db
{
    public class SecurityContext : DbContext
    {
        public const string MigrationsTableName = "_Migrations";

        public SecurityContext(DbContextOptions<SecurityContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<EmployeeEntity> Employees { get; set; }

        public DbSet<EmployeesCarsEntity> EmployeesCars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeEntity).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeesCarsEntity).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host=localhost;Port=5432;Database=security_dev;Username=postgres;Password=1");
        }
    }
}
