using Microsoft.EntityFrameworkCore;
namespace WebApiAngularCore.Models
{
    public class Database : DbContext
    {
        public Database()
        {

        }
        public DbSet<Reference> References { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {               
            optionsBuilder.UseSqlServer("Server=.\\SqlExpress;Database=web;User Id=sa;Password=senha;MultipleActiveResultSets=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {               
            modelBuilder.Entity<Reference>()
                .ToTable("Reference")
                .HasKey(x => x.Id);

            modelBuilder.Entity<Reference>()
                .Property(x => x.Id)
                .IsRequired()
                .UseSqlServerIdentityColumn();

            modelBuilder.Entity<Reference>()
                .Property(x => x.Name)
                .HasMaxLength(50)
                .ForSqlServerHasColumnType("nvarchar(50)");
        }

    }
}
