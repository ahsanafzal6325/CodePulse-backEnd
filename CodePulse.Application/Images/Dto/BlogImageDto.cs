using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulse.Application.Images.Dto
{
    public class BlogImageDto
    {
        public Guid Id { get; set; }
        public string fileName { get; set; }
        public string fileExtention { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public DateTime? createdDate { get; set; }

    }
}
