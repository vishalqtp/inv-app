using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryAPI.Models
{
   

    public class InventoryItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
}

}
