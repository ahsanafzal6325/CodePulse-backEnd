using CodePulse.Application.Images.Dto;
using CodePulse.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulse.Application.Images
{
    public interface IimagesAppService
    {
        Task<BlogImageDto> UploadImage(IFormFile file, string fileName, string title,string rootPath);
        Task<IEnumerable<BlogImageDto>> GetAll();
    }
}
