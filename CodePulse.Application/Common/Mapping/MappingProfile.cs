using AutoMapper;
using CodePulse.Application.Categories.Dto;
using CodePulse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulse.Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCategoryRequest, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<UpdateCategoryRequest, Category>();
        }
    }
}
