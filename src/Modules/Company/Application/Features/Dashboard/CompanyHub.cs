using System.IdentityModel.Tokens.Jwt;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Mcm.Company.Application.Features.Dashboard
{
    public class CompanyHub(
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