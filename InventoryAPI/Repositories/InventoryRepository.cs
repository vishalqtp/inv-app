using InventoryAPI.Models;
using InventoryTracker.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

 namespace InventoryManagement.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly InventoryDbContext _context;

        public InventoryRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InventoryItem>> GetAllItemsAsync()
        {
            return await _context.InventoryItems.ToListAsync();
        }

        public async Task<InventoryItem?> GetItemByIdAsync(int id)
        {
            return await _context.InventoryItems
                                 .FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task AddItemAsync(InventoryItem item)
        {
            await _context.InventoryItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

      public async Task UpdateItemAsync(InventoryItem item)
{
    // Check if the entity is already being tracked by the DbContext
    var existingItem = _context.InventoryItems.Local
        .FirstOrDefault(i => i.Id == item.Id);

    if (existingItem != null)
    {
        // Detach the existing item if it's being tracked
        _context.Entry(existingItem).State = EntityState.Detached;
    }

    // Update the entity
    _context.InventoryItems.Update(item);
    await _context.SaveChangesAsync();
}


        public async Task DeleteItemAsync(int id)
        {
            var item = await GetItemByIdAsync(id);
            if (item != null)
            {
                _context.InventoryItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        // Implementation for SellItemAsync
        public async Task SellItemAsync(int id, int quantity)
        {
            var item = await GetItemByIdAsync(id);
            if (item != null)
            {
                if (item.Quantity >= quantity)
                {
                    item.Quantity -= quantity;
                    await _context.SaveChangesAsync();
                }
            }
        }

        // Implementation for PurchaseItemAsync
        public async Task PurchaseItemAsync(int id, int quantity)
        {
            var item = await GetItemByIdAsync(id);
            if (item != null)
            {
                item.Quantity += quantity;
                await _context.SaveChangesAsync();
            }
        }
    }
}