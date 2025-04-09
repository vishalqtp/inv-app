using System.ComponentModel.DataAnnotations;

namespace InventoryAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        public string? PasswordHash { get; set; }
        [Required]
        public string? Salt { get; set; }
    }
}
