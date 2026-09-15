using Mcm.Authorizations.Application.Features.History.Queries.GetActivityLogs;
using Mcm.Authorizations.Application.Interfaces;
using Mcm.Authorizations.Domain.Entities;
using Mcm.Shared.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Mcm.Authorizations.Application.Features.History.Hubs
{
    public class ActivityLogService(
        ICurrentUserService currentUserService,
        IHubContext<ActivityLogHub> hubContext)
        : IActivityLogService
    {
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IHubContext<ActivityLogHub> _hubContext = hubContext;

        public async Task SendActivityLog(ActivityLogResponse log)
        {
            await _hubContext.Clients.Group($"company:{_currentUserService.CompanyId}").SendAsync("ReceiveActivityLog", log);
        }
    }
}