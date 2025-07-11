using AutoMapper;
using CodePulse.Application.BlogPosts.Dto;
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
            CreateMap<CreateCategoryRequestDto, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<UpdateCategoryRequestDto, Category>();

            CreateMap<CreateBlogPostRequestDto, BlogPost>();
            CreateMap<BlogPost, CreateBlogPostRequestDto>();

        }
    }
}
