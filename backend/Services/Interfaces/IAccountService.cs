using ShoppingCms.Api.DTOs;

namespace ShoppingCms.Api.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountResponse>> GetAccountsAsync();
        Task<AccountResponse?> GetAccountAsync(int id);
        Task<(bool Success, string Message, AccountResponse? Data)> CreateAccountAsync(CreateAccountRequest request);
        Task<(bool Success, string Message)> UpdateAccountAsync(int id, UpdateAccountRequest request);
        Task<(bool Success, string Message)> DeleteAccountAsync(int id);
    }
}
