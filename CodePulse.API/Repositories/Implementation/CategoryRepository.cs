using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            try
            {
                await _dbContext.Categories.AddAsync(category);
                await _dbContext.SaveChangesAsync();
                return category;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            try
            {
                var categories = await _dbContext.Categories.ToListAsync();
                var categoriesDto = categories.Select(c => new Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    UrlHandle = c.UrlHandle
                });
                return categoriesDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
