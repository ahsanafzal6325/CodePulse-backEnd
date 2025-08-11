using CodePulse.Application.Categories;
using CodePulse.Application.Categories.Dto;
using CodePulse.Domain.Entities;
using CodePulse.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    // https://localhost:xxx/api/categories
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryAppService _categoryAppService;
        private readonly ILogger<CategoriesController> _logger;
        public CategoriesController(ICategoryAppService categoryAppService,
            ILogger<CategoriesController> logger)
        {
            _categoryAppService = categoryAppService;
            _logger = logger;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            _logger.LogInformation("Creating category with name {Name}", request.Name);
            var result = await _categoryAppService.CreateAsync(request);
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] CategoryRequestDto request)
        {
            _logger.LogInformation("Fetching all categories");
            var categories = await _categoryAppService.GetAllAsync(request);
            return Ok(categories);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            _logger.LogInformation("Fetching category with ID: {CategoryId}", id);
            var category = await _categoryAppService.GetById(id);
            if (category == null)
            {
                _logger.LogWarning("Category not found with ID: {CategoryId}", id);
                return NotFound();
            }
            return Ok(category);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, UpdateCategoryRequestDto request)
        {
            _logger.LogInformation("Updating category with ID: {CategoryId}", id);
            var category = await _categoryAppService.UpdateAsync(id, request);
            return Ok(category);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            try
            {
                _logger.LogInformation("Deleting category with ID: {CategoryId}", id);
                await _categoryAppService.DeleteAsync(id);
                return Ok("Category Deleted successfully");
            }
            catch (KeyNotFoundException knfEx)
            {
                _logger.LogWarning(knfEx, "Category not found with ID: {CategoryId}", id);
                return NotFound("Category not found");
            }
        }

        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> GetCategoriesCount()
        {
            var count = await _categoryAppService.GetCategoriesCount();
            return Ok(count);
        }

    }
}
