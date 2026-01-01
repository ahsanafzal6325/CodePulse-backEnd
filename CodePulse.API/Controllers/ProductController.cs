using CodePulse.Application.Products;
using CodePulse.Application.Products.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductAppService _productAppService;

        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductAppService productAppService, ILogger<ProductController> logger)
        {
            _productAppService = productAppService;
            _logger = logger;
        }
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> CreateCategory(ProductDto request)
        {
            _logger.LogInformation("Creating product with name {Name}", request.Name);
            var result = await _productAppService.CreateAsync(request);
            return Ok(result);
        }
    }
}
