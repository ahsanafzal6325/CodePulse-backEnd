using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulse.Domain.Entities
{
    public class Comments
    {
        public Guid Id { get; set; }
        public string Content { get; set; }

        public Guid? ParentId { get; set; }

        public Comments ParentComment { get; set; }
        public ICollection<Comments> Replies { get; set; }  
        public Guid PostId { get; set; }  
        public BlogPost Post { get; set; }

        public bool IsDeleted { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? EditBy { get; set; }
        public DateTime? EditDate { get; set; }
    }
}
