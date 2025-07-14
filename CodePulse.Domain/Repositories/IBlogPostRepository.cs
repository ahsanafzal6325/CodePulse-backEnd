using CodePulse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulse.Domain.Repositories
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);
        Task<List<BlogPost>> GetAllAsync();

        Task<BlogPost> GetByIdAsync(Guid id);
        Task<BlogPost> UpdateAsync(BlogPost blogPost);
        Task DeleteAsync(Guid id);
    }
}
