using App1.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace App1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Database.Migrate(); // Applies migrations on startup
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data for UserRoleStatus
            modelBuilder.Entity<UserRoleStatus>().HasData(
                new UserRoleStatus { Id = 1, Name = "Admin", CreateDate = DateTime.UtcNow },
                new UserRoleStatus { Id = 2, Name = "User", CreateDate = DateTime.UtcNow },
                new UserRoleStatus { Id = 3, Name = "Customer", CreateDate = DateTime.UtcNow }
            );

            base.OnModelCreating(modelBuilder);
        }

        // Define your DbSets here
        public DbSet<Product> Products { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<UserRoleStatus> RoleStatuses { get; set; }
    }
}
