using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Domain.Entities;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR;

namespace Mcm.Interactions.Application.Features.Notifications
{
    [Authorize]
    public class InteractionHub(
        IConnectionManager connectionManager,
        IInteractionRepository interactionRepository,
        ICurrentUserService currentUserService)
        : Hub
    {
        private readonly IConnectionManager _connectionManager = connectionManager;
        private readonly IInteractionRepository _interactionRepository = interactionRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        public override async Task OnConnectedAsync()
        {
            var interactions = (await _interactionRepository.GetAllAsync(
                includes: [
                    i => i.InteractionMembers
                ],
                predicate: i =>
                    i.InteractionMembers.Any(im => im.TeamMemberId == _currentUserService.TeamMemberId)
            )).ToList();

            Context.Items["interactionIds"] = interactions.Select(i => i.Id).ToList();
            foreach (var id in (List<Guid>)Context.Items["interactionIds"]!)
                await Groups.AddToGroupAsync(Context.ConnectionId, $"interaction:{id}");
            
            var teamMemberId = Context.User?.FindFirst("team_member_id")?.Value;
            if (!string.IsNullOrEmpty(teamMemberId))
                _connectionManager.AddConnection(teamMemberId, Context.ConnectionId);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (Context.Items.TryGetValue("interactionIds", out var value) && value is List<Guid> ids)
            {
                foreach (var id in ids)
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"interaction:{id}");
            }

            _connectionManager.RemoveConnection(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task AddGroupInteraction(Guid interactionId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"interaction:{interactionId}");
        }
    }
}