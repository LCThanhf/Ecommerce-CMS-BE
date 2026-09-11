using Microsoft.EntityFrameworkCore;
using ShoppingCms.Api.Common;
using ShoppingCms.Api.Data;
using ShoppingCms.Api.DTOs;
using ShoppingCms.Api.Models;
using ShoppingCms.Api.Services.Interfaces;

namespace ShoppingCms.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, string Message, Order? Data)> CreateOrderAsync(int userId, CreateOrderDto dto)
        {
            if (dto.Items == null || !dto.Items.Any())
            {
                return (false, "Giỏ hàng không có sản phẩm.", null);
            }

            var productIds = dto.Items.Select(i => i.ProductId).ToList();
            var productions = await _context.Productions
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();

            var order = new Order
            {
                OrderCode = OrderCodeGenerator.GenerateOrderCode(),
                UserId = userId,
                RecipientName = dto.RecipientName,
                RecipientPhone = dto.RecipientPhone,
                ShippingAddress = dto.ShippingAddress,
                PaymentMethod = string.IsNullOrWhiteSpace(dto.PaymentMethod) ? "COD" : dto.PaymentMethod,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow,
            };

            decimal subTotal = 0;
            foreach (var itemDto in dto.Items)
            {
                var product = productions.FirstOrDefault(p => p.Id == itemDto.ProductId);
                var unitPrice = product != null ? product.Price : itemDto.UnitPrice;
                var itemSubtotal = unitPrice * itemDto.Quantity;
                subTotal += itemSubtotal;

                order.Items.Add(new OrderItem
                {
                    ProductId = itemDto.ProductId,
                    ProductName = product?.Name ?? "Sản phẩm",
                    ProductImage = product?.ImageUrl,
                    UnitPrice = unitPrice,
                    Quantity = itemDto.Quantity,
                    Subtotal = itemSubtotal
                });

                if (product != null)
                {
                    product.StockQuantity -= itemDto.Quantity;
                    if (product.StockQuantity < 0) product.StockQuantity = 0;

                    if (product.StockQuantity <= 5)
                    {
                        var alertType = product.StockQuantity == 0 ? "Hết hàng" : "Sắp hết hàng";
                        var notification = new Notification
                        {
                            Type = "inventory_alert",
                            Title = "Cảnh báo tồn kho",
                            Message = $"Sản phẩm '{product.Name}' {alertType} (còn {product.StockQuantity} cái).",
                            ReferenceId = product.Id.ToString(),
                            CreatedAt = DateTime.UtcNow
                        };
                        _context.Notifications.Add(notification);
                    }
                }
            }

            order.SubTotal = subTotal;
            order.TaxAmount = Math.Round(subTotal * 0.1m, 0);
            order.TotalAmount = order.SubTotal + order.TaxAmount;

            _context.Orders.Add(order);

            var newOrderNotification = new Notification
            {
                Type = "new_order",
                Title = "Đơn hàng mới",
                Message = $"Đơn hàng {order.OrderCode} vừa được tạo với tổng cộng {order.TotalAmount:N0}đ.",
                ReferenceId = order.OrderCode,
                CreatedAt = DateTime.UtcNow
            };
            _context.Notifications.Add(newOrderNotification);

            await _context.SaveChangesAsync();

            return (true, "Đặt hàng thành công", order);
        }

        public async Task<IEnumerable<Order>> GetUserOrdersAsync(int userId)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id, int? userId = null)
        {
            var query = _context.Orders.Include(o => o.Items).AsQueryable();
            if (userId.HasValue)
            {
                query = query.Where(o => o.UserId == userId.Value);
            }
            return await query.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.Items)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<(bool Success, string Message, Order? Data)> UpdateOrderStatusAsync(int id, string status)
        {
            var order = await _context.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return (false, "Không tìm thấy đơn hàng.", null);
            }

            if (status == "Cancelled" && order.Status != "Cancelled")
            {
                foreach (var item in order.Items)
                {
                    var product = await _context.Productions.FindAsync(item.ProductId);
                    if (product != null)
                    {
                        product.StockQuantity += item.Quantity;
                        _context.Productions.Update(product);
                    }
                }
            }

            order.Status = status;
            await _context.SaveChangesAsync();

            return (true, "Cập nhật trạng thái thành công", order);
        }
    }
}
