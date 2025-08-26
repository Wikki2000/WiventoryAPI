using Microsoft.EntityFrameworkCore;
using WiventoryAPI.Models;

namespace WiventoryAPI.Data
{
    /// <summary>
    /// The Entity Framework Core database context for Wiventory.
    /// This class manages the database connection and provides DbSets for all entities.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Users table
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Hotels table
        /// </summary>
        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique index on User.Email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Unique index on Hotel.Email
            modelBuilder.Entity<Hotel>()
                .HasIndex(h => h.Email)
                .IsUnique();

	    // Unique index on Hotel.
	    modelBuilder.Entity<Hotel>()
                .HasIndex(h => h.Subdomain)
                .IsUnique();

            // (Add more unique constraints here as needed)
        }
    }
}

