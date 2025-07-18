using CodePulse.Application.BlogPosts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulse.Application.BlogPosts
{
    public interface IBlogPostsAppService
    {
        Task<BlogPostDto> CreateAsync(CreateBlogPostRequestDto request);

        Task<List<BlogPostDto>> GetAllAsync();
        
        Task<BlogPostDto> GetByIdAsync(Guid id);
        Task<BlogPostDto?> GetByUrlhandle(string urlhandle);

        Task<BlogPostDto> UpdateAsync(Guid id, UpdateBlogPostRequestDto request);
        Task DeleteAsync(Guid id);
    }
}
