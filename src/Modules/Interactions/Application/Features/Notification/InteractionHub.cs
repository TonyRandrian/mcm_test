using System.IdentityModel.Tokens.Jwt;
using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Domain.Entities;
using Mcm.Shared.Application.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR;

namespace Mcm.Interactions.Application.Features.Notification
{
    public class InteractionHub(
        IInteractionRepository interactionRepository)
        : Hub
    {
        private List<Interaction> interactions = [];
        private readonly IInteractionRepository _interactionRepository = interactionRepository;
        public override async Task OnConnectedAsync()
        {
            System.Console.WriteLine("SignalR connected");
            var authorization = Context.GetHttpContext()?
                .Request.Headers["Authorization"]
                .ToString();

            if (string.IsNullOrWhiteSpace(authorization) || !authorization.StartsWith("Bearer "))
                throw new BadRequestException("Invalid Authorization Token");
            var token = authorization["Bearer ".Length..];
           
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var value = jwt.Claims
                .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)
                ?.Value;

            if (!Guid.TryParse(value, out Guid tmId))
                throw new ArgumentException("Can't parse TeamMemberId");
            interactions = (await _interactionRepository.GetAllAsync(
                predicate: i => 
                    i.InteractionMembers.Select(im => im.TeamMemberId).Contains(tmId)
            )).ToList();
            foreach (var i in interactions)
                await AddUserToNotificationGroupAsync(i.Id);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            foreach (var i in interactions)
                await RemoveUserToNotificationGroupAsync(i.Id);
            await base.OnDisconnectedAsync(exception);
        }
        public async Task AddUserToNotificationGroupAsync(Guid interactionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"interaction:{interactionId}");
        }

        public async Task RemoveUserToNotificationGroupAsync(Guid interactionId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"interaction:{interactionId}");
        }
    }
}