using CodePulse.Application.ChatAppService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IChatAppService _chatService;
        private IHttpContextAccessor _httpContextAccessor;

        public ChatController(IChatAppService chatService, IHttpContextAccessor httpContextAccessor)
        {
            _chatService = chatService;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet("history")]
        public async Task<IActionResult> History([FromQuery] int count = 50 , Guid userId = default)
        {
            var senderId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var res = await _chatService.GetRecentAsync(count, userId,senderId);
            return Ok(res);
        }
    }
}
