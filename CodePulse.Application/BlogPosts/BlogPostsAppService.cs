using AutoMapper;
using CodePulse.Application.BlogPosts.Dto;
using CodePulse.Application.Categories.Dto;
using CodePulse.Domain.Entities;
using CodePulse.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulse.Application.BlogPosts
{
    public class BlogPostsAppService : IBlogPostsAppService
    {
        private readonly IMapper _mapper;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly ICategoryRepository _categoryRepository;

        public BlogPostsAppService(IMapper mapper, 
        IBlogPostRepository blogPostRepository,
        ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _blogPostRepository = blogPostRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<BlogPostDto> CreateAsync(CreateBlogPostRequestDto request)
        {
            try
            {
                var blogPost = _mapper.Map<BlogPost>(request);
                if (request.Categories != null && request.Categories.Any())
                {
                    blogPost.Categories = new List<Category>();
                    foreach (var categoryId in request.Categories)
                    {
                        var existingCategory = await _categoryRepository.GetById(categoryId);
                        if (existingCategory is not null)
                        {
                            blogPost.Categories.Add(existingCategory);
                        }
                    }
                }
                var result = await _blogPostRepository.CreateAsync(blogPost);
                var blogPostDto = _mapper.Map<BlogPostDto>(result);

                blogPostDto.Categories = new List<CategoryDto>();
                if (request.Categories != null && request.Categories.Any())
                {
                    foreach (var categoryId in request.Categories)
                    {
                        var existingCategory = await _categoryRepository.GetById(categoryId);
                        var catehoryDto = _mapper.Map<CategoryDto>(existingCategory);
                        if (existingCategory is not null)
                        {
                            blogPostDto.Categories.Add(catehoryDto);
                        }
                    }
                }
                return blogPostDto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<BlogPostDto>> GetAllAsync()
        {
            try
            {
                var blogPosts = await _blogPostRepository.GetAllAsync();
                var blogPostDtos = _mapper.Map<List<BlogPostDto>>(blogPosts);
                return blogPostDtos;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<BlogPostDto> GetByIdAsync(Guid id)
        {
            try
            {
                var blogPost = await _blogPostRepository.GetByIdAsync(id);
                if (blogPost == null)
                {
                    throw new Exception($"Blog post with ID {id} not found.");
                }
                var blogPostDto = _mapper.Map<BlogPostDto>(blogPost);
                blogPostDto.Categories = new List<CategoryDto>();
                if (blogPost.Categories != null && blogPost.Categories.Any())
                {
                    foreach (var category in blogPost.Categories)
                    {
                        var categoryDto = _mapper.Map<CategoryDto>(category);
                        blogPostDto.Categories.Add(categoryDto);
                    }
                }
                return blogPostDto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<BlogPostDto> UpdateAsync(Guid id, UpdateBlogPostRequestDto request)
        {
            try
            {
                var blogPostrequest = _mapper.Map<BlogPost>(request);
                blogPostrequest.Id = id;
                if (request.Categories != null && request.Categories.Any())
                {
                    blogPostrequest.Categories = new List<Category>();
                    foreach (var categoryId in request.Categories)
                    {
                        var existingCategory = await _categoryRepository.GetById(categoryId);
                        if (existingCategory is not null)
                        {
                            blogPostrequest.Categories.Add(existingCategory);
                        }
                    }
                }
                var updatedPost = await _blogPostRepository.UpdateAsync(blogPostrequest);
                return _mapper.Map<BlogPostDto>(updatedPost);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var blogPost = await _blogPostRepository.GetByIdAsync(id);
                if (blogPost == null)
                {
                    throw new Exception($"Blog post with ID {id} not found.");
                }
                await _blogPostRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<BlogPostDto?> GetByUrlhandle(string urlhandle)
        {
            try
            {
                var blogPost = await _blogPostRepository.GetByUrlHandle(urlhandle);
                if (blogPost == null)
                {
                    throw new Exception($"Blog post with url {urlhandle} not found.");
                }
                var blogPostDto = _mapper.Map<BlogPostDto>(blogPost);
                blogPostDto.Categories = new List<CategoryDto>();
                if (blogPost.Categories != null && blogPost.Categories.Any())
                {
                    foreach (var category in blogPost.Categories)
                    {
                        var categoryDto = _mapper.Map<CategoryDto>(category);
                        blogPostDto.Categories.Add(categoryDto);
                    }
                }
                return blogPostDto;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
