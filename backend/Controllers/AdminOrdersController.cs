using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCms.Api.DTOs;
using ShoppingCms.Api.Services.Interfaces;

namespace ShoppingCms.Api.Controllers
{
    [Route("api/admin/orders")]
    [ApiController]
    [Authorize]
    public class AdminOrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public AdminOrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] UpdateOrderStatusDto dto)
        {
            var result = await _orderService.UpdateOrderStatusAsync(id, dto.Status);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }

            return Ok(result.Data);
        }
    }
}
