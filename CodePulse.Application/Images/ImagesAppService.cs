using CodePulse.Application.Images.Dto;
using CodePulse.Domain.Entities;
using CodePulse.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulse.Application.Images
{
    public class ImagesAppService : IimagesAppService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IimageRepository _iimageRepository;
        public ImagesAppService(IHttpContextAccessor httpContextAccessor,
            IimageRepository iimageRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _iimageRepository = iimageRepository;
        }

        public async Task<BlogImageDto> UploadImage(IFormFile file, string fileName, string title, string rootPath)
        {
			try
			{
                var blogImage = new BlogImage
                {
                    FileExtention = Path.GetExtension(file.FileName).ToLower(),
                    FileName = fileName,
                    Title = title,
                    CreatedDate = DateTime.UtcNow,
                };
                var localPath = Path.Combine(rootPath, "Images", $"{blogImage.FileName}{blogImage.FileExtention}");
                var imageFolder = Path.Combine(rootPath, "Images");
                // Ensure directory exists
                if (!Directory.Exists(imageFolder))
                {
                    Directory.CreateDirectory(imageFolder);
                }
                using var stream = new FileStream(localPath, FileMode.Create);

                await file.CopyToAsync(stream);


                var httprequest = _httpContextAccessor.HttpContext.Request;
                var urlPath = $"{httprequest.Scheme}://{httprequest.Host}{httprequest.PathBase}/Images/{blogImage.FileName}{blogImage.FileExtention}";

                blogImage.Url = urlPath;

                blogImage = await _iimageRepository.Upload(blogImage);


                var blogImageDto = new BlogImageDto
                {
                    Id = blogImage.Id,
                    fileName = blogImage.FileName,
                    fileExtention = blogImage.FileExtention,
                    title = blogImage.Title,
                    url = blogImage.Url,
                    createdDate = blogImage.CreatedDate,
                };
                return blogImageDto;
            }
			catch (Exception)
			{
				throw;
			}
        }
        


        public async Task<IEnumerable<BlogImageDto>> GetAll()
        {
            var blogImages = await _iimageRepository.GetAll();
            var blogImageDtos = blogImages.Select(bi => new BlogImageDto
            {
                Id = bi.Id,
                fileName = bi.FileName,
                fileExtention = bi.FileExtention,
                title = bi.Title,
                url = bi.Url,
                createdDate = bi.CreatedDate
            });
            return blogImageDtos;
        }
    }
}
