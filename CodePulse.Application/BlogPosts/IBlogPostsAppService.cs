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
        Task<CreateBlogPostRequestDto> CreateAsync(CreateBlogPostRequestDto request);
    }
}
