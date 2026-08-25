namespace ShoppingCms.Api.DTOs
{
    public class ProductionResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string? ImageUrl { get; set; }
        public string? SubImage1 { get; set; }
        public string? SubImage2 { get; set; }
        public string? SubImage3 { get; set; }
        public decimal Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateProductionRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string? ImageUrl { get; set; }
        public string? SubImage1 { get; set; }
        public string? SubImage2 { get; set; }
        public string? SubImage3 { get; set; }
        public decimal Rating { get; set; }
    }

    public class UpdateProductionRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string? ImageUrl { get; set; }
        public string? SubImage1 { get; set; }
        public string? SubImage2 { get; set; }
        public string? SubImage3 { get; set; }
        public decimal Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
