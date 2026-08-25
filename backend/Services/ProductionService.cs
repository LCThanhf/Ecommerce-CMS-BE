using Microsoft.EntityFrameworkCore;
using ShoppingCms.Api.Data;
using ShoppingCms.Api.DTOs;
using ShoppingCms.Api.Models;
using ShoppingCms.Api.Services.Interfaces;

namespace ShoppingCms.Api.Services
{
    public class ProductionService : IProductionService
    {
        private readonly AppDbContext _context;

        public ProductionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductionResponse>> GetProductionsAsync()
        {
            var productions = await _context.Productions.ToListAsync();
            return productions.Select(MapToResponse);
        }

        public async Task<ProductionResponse?> GetProductionAsync(int id)
        {
            var production = await _context.Productions.FindAsync(id);
            return production == null ? null : MapToResponse(production);
        }

        public async Task<(bool Success, string Message, ProductionResponse? Data)> CreateProductionAsync(CreateProductionRequest request)
        {
            var production = new Production
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                StockQuantity = request.StockQuantity,
                ImageUrl = request.ImageUrl,
                SubImage1 = request.SubImage1,
                SubImage2 = request.SubImage2,
                SubImage3 = request.SubImage3,
                Rating = request.Rating,
                CreatedAt = DateTime.UtcNow
            };

            _context.Productions.Add(production);
            await _context.SaveChangesAsync();

            return (true, "Production created", MapToResponse(production));
        }

        public async Task<(bool Success, string Message)> UpdateProductionAsync(int id, UpdateProductionRequest request)
        {
            var production = await _context.Productions.FindAsync(id);
            if (production == null)
            {
                return (false, "Production not found");
            }

            production.Name = request.Name;
            production.Description = request.Description;
            production.Price = request.Price;
            production.StockQuantity = request.StockQuantity;
            production.ImageUrl = request.ImageUrl;
            production.SubImage1 = request.SubImage1;
            production.SubImage2 = request.SubImage2;
            production.SubImage3 = request.SubImage3;
            production.Rating = request.Rating;

            try
            {
                await _context.SaveChangesAsync();
                return (true, "Production updated");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductionExists(id))
                {
                    return (false, "Production not found");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<(bool Success, string Message)> DeleteProductionAsync(int id)
        {
            var production = await _context.Productions.FindAsync(id);
            if (production == null)
            {
                return (false, "Production not found");
            }

            _context.Productions.Remove(production);
            await _context.SaveChangesAsync();

            return (true, "Production deleted");
        }

        private bool ProductionExists(int id)
        {
            return _context.Productions.Any(e => e.Id == id);
        }

        private ProductionResponse MapToResponse(Production production)
        {
            return new ProductionResponse
            {
                Id = production.Id,
                Name = production.Name,
                Description = production.Description,
                Price = production.Price,
                StockQuantity = production.StockQuantity,
                ImageUrl = production.ImageUrl,
                SubImage1 = production.SubImage1,
                SubImage2 = production.SubImage2,
                SubImage3 = production.SubImage3,
                Rating = production.Rating,
                CreatedAt = production.CreatedAt
            };
        }
    }
}
