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

            CreateMap<BlogPost, BlogPostDto>()
           .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories));

            CreateMap<BlogPostDto, BlogPost>()
                .ForMember(dest => dest.Categories, opt => opt.Ignore());


            CreateMap<BlogPost, CreateBlogPostRequestDto>()
                .ForMember(a => a.Categories, opt => opt.Ignore());

            CreateMap<CreateBlogPostRequestDto, BlogPost>()
                .ForMember(a => a.Categories, opt => opt.Ignore());
          

        }
    }
}
