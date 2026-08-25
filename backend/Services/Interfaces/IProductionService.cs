using ShoppingCms.Api.DTOs;

namespace ShoppingCms.Api.Services.Interfaces
{
    public interface IProductionService
    {
        Task<IEnumerable<ProductionResponse>> GetProductionsAsync();
        Task<ProductionResponse?> GetProductionAsync(int id);
        Task<(bool Success, string Message, ProductionResponse? Data)> CreateProductionAsync(CreateProductionRequest request);
        Task<(bool Success, string Message)> UpdateProductionAsync(int id, UpdateProductionRequest request);
        Task<(bool Success, string Message)> DeleteProductionAsync(int id);
    }
}
