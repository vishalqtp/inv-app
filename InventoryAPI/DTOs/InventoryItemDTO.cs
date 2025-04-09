using System.ComponentModel.DataAnnotations;
using System.Web;

namespace InventoryAPI.DTOs
{
    // public class InventoryItemDTO
    // {
    //     public int Id { get; set; }

    //     [Required]
    //     [StringLength(100, ErrorMessage = "The name cannot be longer than 100 characters.")]
    //     public required string Name { get; set; }

    //     [Required]
    //     public int Quantity { get; set; }

    //     [Required]
    //     public decimal Price { get; set; }
    
    // }

    public class InventoryItemDTO
{
    public int Id { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The name cannot be longer than 100 characters.")]
    public required string Name { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a positive integer.")]
    public int Quantity { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
    public decimal Price { get; set; }

    // Method to sanitize input for XSS prevention
    public void SanitizeInput()
    {
        // Use HttpUtility.HtmlEncode to prevent XSS
        Name = HttpUtility.HtmlEncode(Name); // Sanitizing Name field
    }
}

}
