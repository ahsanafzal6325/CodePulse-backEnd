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
        public async Task<BlogPost> GetByIdAsync(Guid id)
        {
            try
            {
                var blogPost = await _dbContext.BlogPosts
                    .Include(a => a.Categories)
                    .FirstOrDefaultAsync(bp => bp.Id == id);
                if (blogPost == null)
                {
                    throw new KeyNotFoundException("Blog post not found.");
                }
                return blogPost;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the blog post.", ex);
            }
        }
        public async Task<BlogPost> UpdateAsync(BlogPost blogPost)
        {
            try
            {
                var existingBlogPost = await _dbContext.BlogPosts
                    .Include(a => a.Categories)
                    .FirstOrDefaultAsync(bp => bp.Id == blogPost.Id);
                if (existingBlogPost == null)
                {
                    return null; 
                }

                _dbContext.Entry(existingBlogPost).CurrentValues.SetValues(blogPost);

                existingBlogPost.Categories = blogPost.Categories ?? new List<Category>();

                await _dbContext.SaveChangesAsync();

                return existingBlogPost;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the blog post.", ex);
            }
        }
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var blogPost = await _dbContext.BlogPosts.FindAsync(id);
                if (blogPost == null)
                {
                    throw new KeyNotFoundException("Blog post not found.");
                }
                _dbContext.BlogPosts.Remove(blogPost);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the blog post.", ex);
            }
        }

        public async Task<BlogPost?> GetByUrlHandle(string urlHandle)
        {
            try
            {
                var blogPost = await _dbContext.BlogPosts
                    .Include(a => a.Categories)
                    .FirstOrDefaultAsync(bp => bp.UrlHandle == urlHandle);
                if (blogPost == null)
                {
                    throw new KeyNotFoundException("Blog post not found.");
                }
                return blogPost;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
