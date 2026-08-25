using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCms.Api.DTOs;
using ShoppingCms.Api.Services.Interfaces;

namespace ShoppingCms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionsController : ControllerBase
    {
        private readonly IProductionService _productionService;

        public ProductionsController(IProductionService productionService)
        {
            _productionService = productionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductionResponse>>> GetProductions()
        {
            var productions = await _productionService.GetProductionsAsync();
            return Ok(productions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductionResponse>> GetProduction(int id)
        {
            var production = await _productionService.GetProductionAsync(id);

            if (production == null)
            {
                return NotFound();
            }

            return Ok(production);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutProduction(int id, UpdateProductionRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            var result = await _productionService.UpdateProductionAsync(id, request);
            if (!result.Success)
            {
                if (result.Message == "Production not found") return NotFound();
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductionResponse>> PostProduction(CreateProductionRequest request)
        {
            var result = await _productionService.CreateProductionAsync(request);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return CreatedAtAction("GetProduction", new { id = result.Data!.Id }, result.Data);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduction(int id)
        {
            var result = await _productionService.DeleteProductionAsync(id);
            if (!result.Success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
