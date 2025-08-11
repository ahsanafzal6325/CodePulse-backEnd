using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulse.Domain.Entities
{
    public class ChatMessage
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }      
        public string SenderName { get; set; }
        public Guid ReceiverId { get; set; }    
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } 
        public bool IsDeleted { get; set; }

        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? EditBy { get; set; }
        public DateTime? EditDate { get; set; }
    }
}
