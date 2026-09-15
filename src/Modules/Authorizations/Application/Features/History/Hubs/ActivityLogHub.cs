using Mcm.Shared.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Mcm.Authorizations.Application.Features.History.Hubs
{
    public class ActivityLogHub(
        ICurrentUserService currentUserService)
        : Hub
    {
        private readonly ICurrentUserService _currentUserService = currentUserService;
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"company:{_currentUserService.CompanyId}");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"company:{_currentUserService.CompanyId}");
            await base.OnDisconnectedAsync(exception);
        }
    }
}