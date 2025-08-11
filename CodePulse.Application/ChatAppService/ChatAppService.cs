using CodePulse.Application.ChatAppService.Dto;
using CodePulse.Domain.Entities;
using CodePulse.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulse.Application.ChatAppService
{
    public class ChatAppService : IChatAppService
    {
        private readonly IChatRepository _chatRepository;

        public ChatAppService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<ChatMessageDto> SaveMessageAsync(SendMessageDto input, Guid senderId, string senderName)
        {
            var ent = new ChatMessage
            {
                SenderId = senderId,
                SenderName = senderName,
                ReceiverId = input.receiverId,
                Content = input.content,
                CreatedAt = DateTime.UtcNow
            };
            await _chatRepository.CreateAsync(ent);
            return new ChatMessageDto
            {
                Id = ent.Id,
                SenderId = ent.SenderId,
                SenderName = ent.SenderName,
                ReceiverId = ent.ReceiverId,
                Content = ent.Content,
                CreatedAt = ent.CreatedAt
            };
        }

        public async Task<List<ChatMessageDto>> GetRecentAsync(int count = 50,Guid reciverId = default, Guid senderId = default)
        {
            return await _chatRepository.GetRecentAsync(count , reciverId,senderId)
                .ContinueWith(task => task.Result.Select(x => new ChatMessageDto
                {
                    Id = x.Id,
                    SenderId = x.SenderId,
                    SenderName = x.SenderName,
                    ReceiverId = x.ReceiverId,
                    Content = x.Content,
                    CreatedAt = x.CreatedAt
                })
                .ToList());
        }
    }
}
