using AutoMapper;
using CodePulse.Application.BlogPosts.Dto;
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

        public BlogPostsAppService(IMapper mapper, IBlogPostRepository blogPostRepository)
        {
            _mapper = mapper;
            _blogPostRepository = blogPostRepository;
        }

        public async Task<CreateBlogPostRequestDto> CreateAsync(CreateBlogPostRequestDto request)
        {
            try
            {
                var blogPost = _mapper.Map<BlogPost>(request);
                var result = await _blogPostRepository.CreateAsync(blogPost);
                var blogPostDto = _mapper.Map<CreateBlogPostRequestDto>(result);
                return blogPostDto;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
