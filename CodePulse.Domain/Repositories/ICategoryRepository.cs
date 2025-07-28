using CodePulse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulse.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);
        Task<IEnumerable<Category>> GetAllAsync(string? query = null,
            string? sprtBy = null,
            string? sortFor= null,
            int? pageNumber = 1,
            int? pageSize = 100);

        Task<Category?> GetById(Guid id);
        Task UpdateAsync(Category category);

        Task DeleteAsync(Guid id);

        Task<int> GetCategoriesCount();
    }
}
