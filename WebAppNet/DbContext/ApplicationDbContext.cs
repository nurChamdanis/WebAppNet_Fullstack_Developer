using WebAppNet.Models;
using Microsoft.EntityFrameworkCore;
using WebAppNet.Repository;

namespace WebAppNet.DBContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; } // This should point to your Users entity

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRepo>().HasNoKey(); // Define UserRepo as a keyless entity if absolutely necessary
        }
    }
}
