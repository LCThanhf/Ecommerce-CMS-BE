using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCms.Api.DTOs;
using ShoppingCms.Api.Services.Interfaces;

namespace ShoppingCms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<AccountResponse>>> GetAccounts()
        {
            var accounts = await _accountService.GetAccountsAsync();
            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountResponse>> GetAccount(int id)
        {
            var account = await _accountService.GetAccountAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutAccount(int id, UpdateAccountRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            var result = await _accountService.UpdateAccountAsync(id, request);
            if (!result.Success)
            {
                if (result.Message == "Account not found") return NotFound();
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AccountResponse>> PostAccount(CreateAccountRequest request)
        {
            var result = await _accountService.CreateAccountAsync(request);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return CreatedAtAction("GetAccount", new { id = result.Data!.Id }, result.Data);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var result = await _accountService.DeleteAccountAsync(id);
            if (!result.Success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
