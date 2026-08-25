using ShoppingCms.Api.DTOs;

namespace ShoppingCms.Api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<(bool Success, string Message)> RegisterAsync(RegisterRequest request);
        Task<(bool Success, string Message, AuthResponse? Data)> LoginAsync(LoginRequest request);
    }
}
