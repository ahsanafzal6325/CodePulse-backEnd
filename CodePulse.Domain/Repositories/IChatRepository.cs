using CodePulse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulse.Domain.Repositories
{
    public interface IChatRepository
    {
        Task<ChatMessage> CreateAsync(ChatMessage chat);
        Task<List<ChatMessage>> GetRecentAsync(int count = 50,Guid userId = default,Guid senderId = default);
    }
}
