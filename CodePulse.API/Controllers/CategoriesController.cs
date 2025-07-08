using CodePulse.Application.Categories;
using CodePulse.Application.Categories.Dto;
using CodePulse.Domain.Entities;
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
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
        {
            try
            {
                _logger.LogInformation("Creating category with name {Name} in API", request.Name);
                var result = await _categoryAppService.CreateAsync(request);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet]

        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _categoryAppService.GetAllAsync();
                return Ok(categories);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            try
            {
                var category = await _categoryAppService.GetById(id);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, UpdateCategoryRequest request)
        {
            try
            {
                var category = await _categoryAppService.UpdateAsync(id,request);
                return Ok(category);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
