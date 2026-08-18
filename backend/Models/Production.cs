using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCms.Api.Models
{
    public class Production
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;
        
        public string? Description { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        public int StockQuantity { get; set; }
        
        [MaxLength(500)]
        public string? ImageUrl { get; set; }
        
        [Column(TypeName = "decimal(3,1)")]
        public decimal Rating { get; set; } = 0; // e.g., 4.5
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
