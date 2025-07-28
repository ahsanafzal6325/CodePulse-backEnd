using CodePulse.Application.Categories;
using CodePulse.Application.Categories.Dto;
using CodePulse.Domain.Entities;
using CodePulse.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public CategoriesController(ICategoryAppService categoryAppService, ILogger<CategoriesController> logger)
        {
            _categoryAppService = categoryAppService;
            _logger = logger;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            try
            {
                _logger.LogInformation("Creating category with name {Name}", request.Name);
                var result = await _categoryAppService.CreateAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating category with name {Name}", request.Name);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] CategoryRequestDto request)
        {
            try
            {
                var role = UserRolesEnum.Reader.GetDescription();
                _logger.LogInformation("Fetching all categories");
                var categories = await _categoryAppService.GetAllAsync(request);
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all categories");
                return StatusCode(500, "An error occurred while processing your request.");
            }

        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching category with ID: {CategoryId}", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, UpdateCategoryRequestDto request)
        {
            try
            {
                _logger.LogInformation("Updating category with ID: {CategoryId}", id);
                var category = await _categoryAppService.UpdateAsync(id, request);
                return Ok(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating category with ID: {CategoryId}", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting category with ID: {CategoryId}", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
