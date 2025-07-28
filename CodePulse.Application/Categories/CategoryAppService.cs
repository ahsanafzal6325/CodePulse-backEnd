using AutoMapper;
using CodePulse.Application.Categories.Dto;
using CodePulse.Domain.Entities;
using CodePulse.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulse.Application.Categories
{
    public class CategoryAppService : ICategoryAppService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryAppService> _logger;
        public CategoryAppService(ICategoryRepository categoryRepository, IMapper mapper, ILogger<CategoryAppService> logger)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Category> CreateAsync(CreateCategoryRequestDto request)
        {
            try
            {
                var category = _mapper.Map<Category>(request);
                var result = await _categoryRepository.CreateAsync(category);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IEnumerable<CategoryDto>> GetAllAsync(CategoryRequestDto request)
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync(request.query,request.sortBy ,request.sortDirection);
                var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);
                return categoriesDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<CategoryDto> GetById(Guid id)
        {
            try
            {
                var category = await _categoryRepository.GetById(id);
                var categoryDto = _mapper.Map<CategoryDto>(category);
                return categoryDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Category> UpdateAsync(Guid id, UpdateCategoryRequestDto request)
        {
            try
            {
                var category = await _categoryRepository.GetById(id);
                if (category == null)
                {
                    throw new KeyNotFoundException("Category not found");
                }
                _mapper.Map(request, category);
                await _categoryRepository.UpdateAsync(category);
                return category;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var category = await _categoryRepository.GetById(id);
                if (category == null)
                {
                    throw new KeyNotFoundException("Category not found");
                }
                await _categoryRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
