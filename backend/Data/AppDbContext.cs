using Microsoft.EntityFrameworkCore;
using ShoppingCms.Api.Models;

namespace ShoppingCms.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<Production> Productions { get; set; } = null!;
    }
}
