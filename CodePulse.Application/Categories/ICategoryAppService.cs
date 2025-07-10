using CodePulse.Application.Categories.Dto;
using CodePulse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulse.Application.Categories
{
    public interface ICategoryAppService
    {
        Task<Category> CreateAsync(CreateCategoryRequestDto category);
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetById(Guid id);
        Task<Category> UpdateAsync(Guid id, UpdateCategoryRequestDto request);
        Task<bool> DeleteAsync(Guid id);
    }
}
