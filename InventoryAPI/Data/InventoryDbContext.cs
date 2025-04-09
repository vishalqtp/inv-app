using InventoryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryTracker.Data
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }

        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<User> Users { get; set; } 

    }
}
