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
        public async Task<IEnumerable<Category>> GetAllAsync(string? query = null,
            string? sortBy = null,
            string? sortDirection = null,
            int? pageNumber = 1,
            int? pageSize = 100)
        {
            try
            {
                // Query
                var categories = _dbContext.Categories.AsQueryable();

                // Filtering 
                if (!string.IsNullOrWhiteSpace(query)) 
                {
                    categories = categories.Where(x => x.Name.Contains(query));
                }

                //Sorting
                if (!string.IsNullOrWhiteSpace(sortBy))
                {
                    if (string.Equals(sortBy,"Name", StringComparison.OrdinalIgnoreCase))
                    {
                        var isAsceding = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;

                        categories = isAsceding ? categories.OrderBy(x => x.Name) : categories
                            .OrderByDescending(x => x.Name);
                    }
                    if (string.Equals(sortBy, "URL", StringComparison.OrdinalIgnoreCase))
                    {
                        var isAsceding = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;

                        categories = isAsceding ? categories.OrderBy(x => x.UrlHandle) : categories
                            .OrderByDescending(x => x.UrlHandle);
                    }
                }
                // Pagination
                var skip = (pageNumber - 1) * pageSize;
                categories = categories.Skip(skip ?? 0).Take(pageSize ?? 100);

                //var categories = await _dbContext.Categories.ToListAsync();
                return await categories.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<Category?> GetById(Guid id)
        {
            try
            {
                var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
                return category;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task UpdateAsync(Category category)
        {
            try
            {
                _dbContext.Categories.Update(category);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var category = await _dbContext.Categories.FindAsync(id);
                if (category != null)
                {
                    _dbContext.Categories.Remove(category);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> GetCategoriesCount()
        {
            try
            {
                return await _dbContext.Categories.CountAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
