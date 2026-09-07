using ShoppingCms.Api.DTOs;
using ShoppingCms.Api.Models;

namespace ShoppingCms.Api.Services.Interfaces
{
    public interface IOrderService
    {
        Task<(bool Success, string Message, Order? Data)> CreateOrderAsync(int userId, CreateOrderDto dto);
        Task<IEnumerable<Order>> GetUserOrdersAsync(int userId);
        Task<Order?> GetOrderByIdAsync(int id, int? userId = null);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<(bool Success, string Message, Order? Data)> UpdateOrderStatusAsync(int id, string status);
    }
}
