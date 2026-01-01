using CodePulse.Application.Categories.Dto;
using CodePulse.Application.Products.Dto;
using CodePulse.Domain.Entities;

namespace CodePulse.Application.Products
{
    public interface IProductAppService
    {
        Task<Product> CreateAsync(ProductDto productDto);
    }
}
