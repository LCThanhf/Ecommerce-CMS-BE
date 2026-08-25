using Microsoft.EntityFrameworkCore;
using ShoppingCms.Api.Data;
using ShoppingCms.Api.DTOs;
using ShoppingCms.Api.Models;
using ShoppingCms.Api.Services.Interfaces;

namespace ShoppingCms.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _context;

        public AccountService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AccountResponse>> GetAccountsAsync()
        {
            var accounts = await _context.Accounts.ToListAsync();
            return accounts.Select(MapToResponse);
        }

        public async Task<AccountResponse?> GetAccountAsync(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            return account == null ? null : MapToResponse(account);
        }

        public async Task<(bool Success, string Message, AccountResponse? Data)> CreateAccountAsync(CreateAccountRequest request)
        {
            var account = new Account
            {
                Username = request.Username,
                Name = request.Name,
                Email = request.Email,
                PasswordHash = request.PasswordHash, 
                Role = request.Role,
                Gender = request.Gender,
                HomeAddress = request.HomeAddress,
                WorkAddress = request.WorkAddress,
                Dob = request.Dob,
                Phone = request.Phone,
                Avatar = request.Avatar,
                CreatedAt = DateTime.UtcNow
            };

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return (true, "Account created", MapToResponse(account));
        }

        public async Task<(bool Success, string Message)> UpdateAccountAsync(int id, UpdateAccountRequest request)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return (false, "Account not found");
            }

            account.Username = request.Username;
            account.Name = request.Name;
            account.Email = request.Email;
            account.PasswordHash = request.PasswordHash;
            account.Role = request.Role;
            account.Gender = request.Gender;
            account.HomeAddress = request.HomeAddress;
            account.WorkAddress = request.WorkAddress;
            account.Dob = request.Dob;
            account.Phone = request.Phone;
            account.Avatar = request.Avatar;
            
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Account updated");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
                {
                    return (false, "Account not found");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<(bool Success, string Message)> DeleteAccountAsync(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return (false, "Account not found");
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return (true, "Account deleted");
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }

        private AccountResponse MapToResponse(Account account)
        {
            return new AccountResponse
            {
                Id = account.Id,
                Username = account.Username,
                Name = account.Name,
                Email = account.Email,
                Role = account.Role,
                Gender = account.Gender,
                HomeAddress = account.HomeAddress,
                WorkAddress = account.WorkAddress,
                Dob = account.Dob,
                Phone = account.Phone,
                Avatar = account.Avatar,
                CreatedAt = account.CreatedAt
            };
        }
    }
}
