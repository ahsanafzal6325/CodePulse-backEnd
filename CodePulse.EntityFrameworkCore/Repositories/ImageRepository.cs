using CodePulse.Domain.Entities;
using CodePulse.Domain.Repositories;
using CodePulse.EntityFrameworkCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulse.EntityFrameworkCore.Repositories
{
    public class ImageRepository : IimageRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ImageRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BlogImage> Upload(BlogImage blogImage)
        {
            try
            {
                await _dbContext.BlogImages.AddAsync(blogImage);
                await _dbContext.SaveChangesAsync();
                return blogImage;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        public async Task<IEnumerable<BlogImage>> GetAll()
        {
            try
            {
                return await _dbContext.BlogImages.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
