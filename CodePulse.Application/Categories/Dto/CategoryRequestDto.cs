using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulse.Application.Categories.Dto
{
    public class CategoryRequestDto
    {
        public string? query { get; set; }
        public string? sortBy { get; set; }
        public string? sortDirection { get; set; }
    }
}
