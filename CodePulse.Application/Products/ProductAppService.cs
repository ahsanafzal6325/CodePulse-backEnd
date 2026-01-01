using AutoMapper;
using CodePulse.Application.Products.Dto;
using CodePulse.Domain.Entities;
using CodePulse.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulse.Application.Products
{
    public class ProductAppService : IProductAppService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;


        public ProductAppService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Product> CreateAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            var result = await _productRepository.CreateAsync(product);
            return result;
        }
    }
}
