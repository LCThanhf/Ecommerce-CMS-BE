using System.ComponentModel.DataAnnotations;

namespace ShoppingCms.Api.DTOs
{
    public class CreateOrderItemDto
    {
        [Required]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }

    public class CreateOrderDto
    {
        [Required]
        public string RecipientName { get; set; } = string.Empty;

        [Required]
        public string RecipientPhone { get; set; } = string.Empty;

        [Required]
        public string ShippingAddress { get; set; } = string.Empty;

        public string PaymentMethod { get; set; } = "COD";

        [Required]
        public List<CreateOrderItemDto> Items { get; set; } = new();
    }

    public class UpdateOrderStatusDto
    {
        [Required]
        public string Status { get; set; } = string.Empty;
    }
}
