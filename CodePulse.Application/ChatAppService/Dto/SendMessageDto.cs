using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulse.Application.ChatAppService.Dto
{
    public class SendMessageDto
    {
        public Guid receiverId { get; set; } // optional
        public string content { get; set; }
        public string email { get; set; }
    }
}
