using Microsoft.AspNetCore.SignalR;

namespace Mcm.Shared.Application.Extensions
{
    public class SignalRExtension
        : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            var result = connection.User?.FindFirst("team_member_id")?.Value;
            return result;
        }
    }
}