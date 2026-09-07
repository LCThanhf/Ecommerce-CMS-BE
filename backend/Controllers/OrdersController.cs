using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCms.Api.DTOs;
using ShoppingCms.Api.Services.Interfaces;
using System.Security.Claims;

namespace ShoppingCms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        private int GetCurrentUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier) 
                     ?? User.FindFirst("sub") 
                     ?? User.FindFirst("id");
            return (claim != null && int.TryParse(claim.Value, out int id)) ? id : 0;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
        {
            var userId = GetCurrentUserId();
            if (userId == 0) return Unauthorized();

            var result = await _orderService.CreateOrderAsync(userId, dto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = GetCurrentUserId();
            if (userId == 0) return Unauthorized();

            var orders = await _orderService.GetUserOrdersAsync(userId);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var userId = GetCurrentUserId();
            if (userId == 0) return Unauthorized();

            var order = await _orderService.GetOrderByIdAsync(id, userId);
            if (order == null) return NotFound("Không tìm thấy đơn hàng.");

            return Ok(order);
        }
    }
}
