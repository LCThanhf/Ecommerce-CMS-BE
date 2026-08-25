namespace ShoppingCms.Api.DTOs
{
    public class AccountResponse
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string? Gender { get; set; }
        public string? HomeAddress { get; set; }
        public string? WorkAddress { get; set; }
        public string? Dob { get; set; }
        public string? Phone { get; set; }
        public string? Avatar { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateAccountRequest
    {
        public string Username { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty; // Keeping original name for compatibility, even if it's plaintext sent by frontend
        public string Role { get; set; } = "User";
        public string? Gender { get; set; }
        public string? HomeAddress { get; set; }
        public string? WorkAddress { get; set; }
        public string? Dob { get; set; }
        public string? Phone { get; set; }
        public string? Avatar { get; set; }
    }

    public class UpdateAccountRequest
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty; 
        public string Role { get; set; } = string.Empty;
        public string? Gender { get; set; }
        public string? HomeAddress { get; set; }
        public string? WorkAddress { get; set; }
        public string? Dob { get; set; }
        public string? Phone { get; set; }
        public string? Avatar { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
