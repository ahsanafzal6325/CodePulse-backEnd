using CodePulse.Domain.Entities;
using CodePulse.Domain.Repositories;
using CodePulse.EntityFrameworkCore.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulse.EntityFrameworkCore.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BlogPostRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            try
            {
                await _dbContext.BlogPosts.AddAsync(blogPost);
                await _dbContext.SaveChangesAsync();
                return blogPost; 
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the blog post.", ex);
            }
        }
        public async Task<List<BlogPost>> GetAllAsync()
        {
            try
            {
                var blogPosts = await _dbContext.BlogPosts.Include(a => a.Categories).ToListAsync();
                return blogPosts;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the blog posts.", ex);
            }
        }
    }
}
