using Microsoft.EntityFrameworkCore;
using ShoppingCms.Api.Models;

namespace ShoppingCms.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<Production> Productions { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>().HasIndex(o => o.OrderCode).IsUnique();

            modelBuilder.Entity<Account>().HasData(new Account
            {
                Id = 1,
                Username = "Admin",
                Email = "admin@gmail.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                Role = "Admin",
                CreatedAt = new System.DateTime(2023, 1, 1, 0, 0, 0, System.DateTimeKind.Utc)
            });
        }
    }
}
