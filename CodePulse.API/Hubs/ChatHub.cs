using CodePulse.Application.ChatAppService;
using CodePulse.Application.ChatAppService.Dto;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace CodePulse.API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatAppService _chatService;
        public ChatHub(IChatAppService chatService) { _chatService = chatService; }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Readers");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Readers");
            await base.OnDisconnectedAsync(exception);
        }
        public async Task SendMessage(SendMessageDto message)
        {
            var senderId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                       ?? Context.User?.FindFirst("sub")?.Value;
            var senderName = Context.User?.Identity?.Name ?? senderId ?? "Unknown";
            var saved = await _chatService.SaveMessageAsync(message, Guid.Parse(senderId), senderName);

            if (!string.IsNullOrEmpty(message.receiverId.ToString()))
            {
                await Clients.User(message.receiverId.ToString()).SendAsync("ReceiveMessage", saved);
                await Clients.Caller.SendAsync("ReceiveMessage", saved); 
            }
            else
            {
                await Clients.Group("Readers").SendAsync("ReceiveMessage", saved);
            }
        }
    }
}
