using InventoryAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagement.Repositories
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<InventoryItem>> GetAllItemsAsync();
        Task<InventoryItem?> GetItemByIdAsync(int id);
        Task AddItemAsync(InventoryItem item);
        Task UpdateItemAsync(InventoryItem item);
        Task DeleteItemAsync(int id);
        Task SellItemAsync(int id, int quantity);  // Added method to handle selling an item
        Task PurchaseItemAsync(int id, int quantity);  // Added method to handle purchasing an item
    }
}
