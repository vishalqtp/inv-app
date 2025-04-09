using AutoMapper;
using InventoryAPI.Models;
using InventoryAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InventoryAPI.Services;

namespace InventoryManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        private readonly IMapper _mapper;

        public InventoryController(IInventoryService inventoryService, IMapper mapper)
        {
            _inventoryService = inventoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItemDTO>>> GetAllItems()
        {
            var items = await _inventoryService.GetAllAsync();
            if (items == null || !items.Any())
                return NotFound(new { message = "No inventory items found." });

            var itemsDto = _mapper.Map<IEnumerable<InventoryItemDTO>>(items);
            return Ok(itemsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryItemDTO>> GetItemById(int id)
        {
            var item = await _inventoryService.GetByIdAsync(id);
            if (item == null)
                return NotFound(new { message = $"Item with id {id} not found" });

            var itemDto = _mapper.Map<InventoryItemDTO>(item);
            return Ok(itemDto);
        }

        [HttpPost]
        public async Task<ActionResult> AddItem([FromBody] InventoryItemDTO itemDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            itemDto.SanitizeInput();
            var item = _mapper.Map<InventoryItem>(itemDto);

            await _inventoryService.AddAsync(item);

            return CreatedAtAction(nameof(GetItemById), new { id = item.Id }, itemDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItem(int id, [FromBody] InventoryItemDTO itemDto)
        {
            if (id != itemDto.Id)
                return BadRequest(new { message = "Item ID mismatch" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingItem = await _inventoryService.GetByIdAsync(id);
            if (existingItem == null)
                return NotFound(new { message = $"Item with id {id} not found" });

            var item = _mapper.Map<InventoryItem>(itemDto);

            await _inventoryService.UpdateAsync(item);

            return NoContent();
        }

        [HttpPatch("sell/{id}")]
        public async Task<ActionResult> SellItem(int id, [FromBody] SellItemRequestDTO request)
        {
            if (request == null || request.Quantity <= 0)
            {
                return BadRequest(new { message = "Invalid quantity. It must be greater than zero." });
            }

            try
            {
                await _inventoryService.SellItemAsync(id, request.Quantity);
                return Ok(new { message = "Sale completed successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", error = ex.Message });
            }
        }

        [HttpPatch("purchase/{id}")]
        public async Task<ActionResult> PurchaseItem(int id, [FromBody] PurchaseItemRequestDTO request)
        {
            if (request == null || request.Quantity <= 0)
            {
                return BadRequest(new { message = "Invalid quantity. It must be greater than zero." });
            }

            try
            {
                await _inventoryService.PurchaseItemAsync(id, request.Quantity);
                return Ok(new { message = "Purchase completed successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            var existingItem = await _inventoryService.GetByIdAsync(id);
            if (existingItem == null)
                return NotFound(new { message = $"Item with id {id} not found" });

            await _inventoryService.DeleteAsync(id);
            return Ok(new { message = "Item deleted successfully", id = id });
        }
    }
}

