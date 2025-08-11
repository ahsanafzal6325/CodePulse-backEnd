using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace CodePulse.API.Hubs
{
    public class SubUserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            // typical JWT: sub claim
            return connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                   ?? connection.User?.FindFirst("sub")?.Value;
        }
    }
}
