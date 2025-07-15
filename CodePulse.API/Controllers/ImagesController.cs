using CodePulse.Application.Images;
using CodePulse.Application.Images.Dto;
using CodePulse.Domain.Entities;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        //POST /api/images
        private readonly IimagesAppService _iimagesAppService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ImagesController(IimagesAppService iimagesAppService, IWebHostEnvironment webHostEnvironment)
        {
            _iimagesAppService = iimagesAppService;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            try
            {
                var blogImagesDto = await _iimagesAppService.GetAll();
                return Ok(blogImagesDto);
            }
            catch (Exception)
            {

                throw;
            }
        }




        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] UploadImageDto input)
        {
            try
            {
                ValidateFielUpload(input.file);
                if (ModelState.IsValid)
                {
                    var blogImage = await _iimagesAppService.UploadImage(input.file, input.fileName, input.title, _webHostEnvironment.ContentRootPath);
                    return Ok(blogImage);
                }
                return BadRequest(ModelState);
            }
            catch (Exception)
            {
                throw;
            }
          
        }
        private void ValidateFielUpload(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
            {
                ModelState.AddModelError("file", "Unsupported file format. Only JPG, JPEG, PNG, and GIF are allowed.");
            }

            if (file.Length > 10 * 1024 * 1024) // 10MB
            {
                ModelState.AddModelError("file", "File size cannot be more than 10MB.");
            }
        }

    }
}
