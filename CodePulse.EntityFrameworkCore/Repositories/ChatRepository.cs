using CodePulse.Domain.Entities;
using CodePulse.Domain.Repositories;
using CodePulse.EntityFrameworkCore.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulse.EntityFrameworkCore.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ChatRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ChatMessage> CreateAsync(ChatMessage chat)
        {
            try
            {
                await _dbContext.ChatMessages.AddAsync(chat);
                await _dbContext.SaveChangesAsync();
                return chat;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the chat message.", ex);
            }
        }
        public async Task<List<ChatMessage>> GetRecentAsync(int count = 50, Guid reciverId = default, Guid senderId = default)
        {
            try
            {
                return await _dbContext.ChatMessages
                    .Where(a => a.ReceiverId == reciverId && a.SenderId == senderId || a.ReceiverId == senderId && a.SenderId == reciverId)
                    .Take(count)
                    .OrderBy(a => a.CreatedDate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving recent chat messages.", ex);
            }
        }
    }
}
