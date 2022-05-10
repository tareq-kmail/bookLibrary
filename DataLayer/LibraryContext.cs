using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data
{
    public class LibraryContext : DbContext
    {

        public DbSet<AdminEntity> Admins { get; set; }
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<RentalEntity> Rental { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<SalesEntity> Sales { get; set; }

        public LibraryContext(DbContextOptions<LibraryContext> options ) : base(options)
        {
          
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
           
        //    optionsBuilder.UseSqlServer("Server=SD-LP-W10-TAKMA;Database=LibraryDB;Trusted_Connection=True;",
        //                                                                    b => b.MigrationsAssembly("Data"));
        //}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AdminEntity>(entity => {
                entity.HasIndex(e => e.Username).IsUnique();
            });
            builder.Entity<BookEntity>(entity => {
                entity.HasIndex(e => e.Isbn).IsUnique();
            });
            builder.Entity<CustomerEntity>(entity => {
                 entity.HasIndex(e => e.NationalId).IsUnique();
            });
        }

    }
    
}

