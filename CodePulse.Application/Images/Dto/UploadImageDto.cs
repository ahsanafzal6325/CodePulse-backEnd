using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulse.Application.Images.Dto
{
    public class UploadImageDto
    {
        [FromForm]
        public IFormFile file { get; set; }

        [FromForm]
        public string fileName { get; set; }

        [FromForm]
        public string title { get; set; }
    }
}
