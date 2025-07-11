﻿using CodePulse.Application.BlogPosts;
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
                _logger.LogInformation("Creating a new blog post with title: {Title}", request.Title);
                var result = await _blogPostAppService.CreateAsync(request);
                _logger.LogInformation("BlogPost created successfully with title: {Title}", request.Title);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("An error occurred while creating the blog post with title: {Title}. Exception: {Exception}", request.Title, ex.Message);
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
    }
}
