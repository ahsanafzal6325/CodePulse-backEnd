using CodePulse.Application.ChatAppService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulse.Application.ChatAppService
{
    public interface IChatAppService
    {
        Task<ChatMessageDto> SaveMessageAsync(SendMessageDto input, Guid senderId, string senderName);
        Task<List<ChatMessageDto>> GetRecentAsync(int count = 50,Guid userId = default,Guid senderId = default);
    }
}
