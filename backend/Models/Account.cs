using System.ComponentModel.DataAnnotations;

namespace ShoppingCms.Api.Models
{
    public class Account
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;
        
        [MaxLength(100)]
        public string? Name { get; set; } // Added for frontend sync

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string Role { get; set; } = "User"; // Admin or User
        
        [MaxLength(20)]
        public string? Gender { get; set; } // Male, Female, Other
        
        [MaxLength(500)]
        public string? HomeAddress { get; set; }
        
        [MaxLength(500)]
        public string? WorkAddress { get; set; }
        
        [MaxLength(20)]
        public string? Dob { get; set; } // YYYY/MM/DD

        [MaxLength(20)]
        public string? Phone { get; set; }

        [MaxLength(500)]
        public string? Avatar { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
