using InventoryAPI.Models;
using InventoryManagement.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryAPI.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repository;

        public InventoryService(IInventoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<InventoryItem>> GetAllAsync()
        {
            return await _repository.GetAllItemsAsync();
        }

        public async Task<InventoryItem?> GetByIdAsync(int id)
        {
            return await _repository.GetItemByIdAsync(id);
        }

        public async Task AddAsync(InventoryItem item)
        {
            await _repository.AddItemAsync(item);
        }

        public async Task UpdateAsync(InventoryItem item)
        {
            await _repository.UpdateItemAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteItemAsync(id);
        }

        public async Task SellItemAsync(int id, int quantity)
        {
            // Logic to handle selling an item (e.g., checking stock, updating inventory)
            var item = await _repository.GetItemByIdAsync(id);
            if (item == null)
                throw new KeyNotFoundException($"Item with id {id} not found.");

            if (item.Quantity < quantity)
                throw new InvalidOperationException("Not enough stock available to complete the sale.");

            item.Quantity -= quantity;  // Decrease stock by quantity sold
            await _repository.UpdateItemAsync(item); // Update the item in the repository
        }

        public async Task PurchaseItemAsync(int id, int quantity)
        {
            // Logic to handle purchasing an item (e.g., increasing stock)
            var item = await _repository.GetItemByIdAsync(id);
            if (item == null)
                throw new KeyNotFoundException($"Item with id {id} not found.");

            item.Quantity += quantity;  // Increase stock by purchased quantity
            await _repository.UpdateItemAsync(item); // Update the item in the repository
        }
    }
}
