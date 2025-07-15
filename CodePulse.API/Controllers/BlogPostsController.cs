using CodePulse.Application.BlogPosts;
using CodePulse.Application.BlogPosts.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostsAppService _blogPostAppService;
        private readonly ILogger<BlogPostsController> _logger;
        public BlogPostsController(IBlogPostsAppService blogPostAppService, ILogger<BlogPostsController> logger)
        {
            _blogPostAppService = blogPostAppService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogPost(CreateBlogPostRequestDto request)
        {
            try
            {
                _logger.LogInformation("Creating a new blog post with title: {title}", request.Title);
                var result = await _blogPostAppService.CreateAsync(request);
                _logger.LogInformation("BlogPost created successfully with title: {title}", request.Title);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("An error occurred while creating the blog post with title: {title}. Exception: {Exception}", request.Title, ex.Message);
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpGet]
        public async Task<List<BlogPostDto>> GetAllBlogPosts()
        {
            try
            {
                _logger.LogInformation("Retrieving all blog posts");
                var result = await _blogPostAppService.GetAllAsync();
                _logger.LogInformation("Retrieved {Count} blog posts", result.Count);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while retrieving blog posts: {Exception}", ex.Message);
                throw new Exception($"An error occurred while retrieving blog posts: {ex.Message}");
            }
        }


        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
        {
            try
            {
                _logger.LogInformation("Retrieving blog post with ID: {Id}", id);
                var result = await _blogPostAppService.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("Blog post with ID: {Id} not found", id);
                    return NotFound();
                }
                _logger.LogInformation("Retrieved blog post with ID: {Id}", id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while retrieving blog post with ID: {Id}. Exception: {Exception}", id, ex.Message);
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        // PUT: api/BlogPosts/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateBlogPost(Guid id, UpdateBlogPostRequestDto request)
        {
            try
            {
                _logger.LogInformation("Updating blog post with ID: {Id}", id);
                var result = await _blogPostAppService.UpdateAsync(id, request);
                if (result == null)
                {
                    _logger.LogWarning("Blog post with ID: {Id} not found", id);
                    return NotFound();
                }
                _logger.LogInformation("Updated blog post with ID: {Id}", id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while updating blog post with ID: {Id}. Exception: {Exception}", id, ex.Message);
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // DELETE: api/BlogPosts/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteBlogPost(Guid id)
        {
            try
            {
                _logger.LogInformation("Deleting blog post with ID: {Id}", id);
                var result = await _blogPostAppService.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("Blog post with ID: {Id} not found", id);
                    return NotFound();
                }
                // Assuming you have a method to delete the blog post in your service
                await _blogPostAppService.DeleteAsync(id);
                _logger.LogInformation("Deleted blog post with ID: {Id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while deleting blog post with ID: {Id}. Exception: {Exception}", id, ex.Message);
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
