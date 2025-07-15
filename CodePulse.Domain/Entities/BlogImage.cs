using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulse.Domain.Entities
{
    public class BlogImage
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileExtention { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public bool IsDeleted { get; set; }

        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? EditBy { get; set; }
        public DateTime? EditDate { get; set; }


    }
}
