using InventoryAPI.Models;
namespace InventoryAPI.Services
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryItem>> GetAllAsync();
        Task<InventoryItem?> GetByIdAsync(int id);
        Task AddAsync(InventoryItem item);
        Task UpdateAsync(InventoryItem item);
        Task DeleteAsync(int id);
        Task SellItemAsync(int id, int quantity);
        Task PurchaseItemAsync(int id, int quantity);
    }
}
