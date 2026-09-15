using Mcm.Authorizations.Application.Interfaces;
using Mcm.Authorizations.Domain.Entities;
using Mcm.Authorizations.Domain.Extensions;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Domain.Events;
using MediatR;

namespace Mcm.Authorizations.Application.Features.TeamMembers.Commands.Events
{
    public class DefineTeamMemberAsLeaderEventHandler(
        ITeamMemberRepository teamMemberRepository,
        IAuthorizationUow uow,
        IRoleRepository roleRepository)
        : INotificationHandler<LeaderDefinedEvent>
    {
        private readonly ITeamMemberRepository _teamMemberRepository = teamMemberRepository;
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IAuthorizationUow _uow = uow;

        public async Task Handle(LeaderDefinedEvent notification, CancellationToken cancellationToken)
        {
            var teamMember = await _teamMemberRepository.GetByIdAsync(notification.TeamMemberId)
                ?? throw NotFoundException.NotFoundById(nameof(TeamMember), notification.TeamMemberId);
            
            var role = await _roleRepository.Validate(r => 
                r.Title.Value.Equals("Dirigeant") &&
                r.Description.Equals($"Role dirigeant de l'entreprise {notification.CompanyName}"));
            
            if (role is null)
            {
                role = Role.Create("Dirigeant", $"Role dirigeant de l'entreprise {notification.CompanyName}", true);
                role.AddManyPermission(LeaderPermission.GetValues());
                role.AddCompany(notification.CompanyId);
                await _roleRepository.AddAsync(role);
            }
            else
            {
                role.RemoveMember(role.Members.FirstOrDefault()!.TeamMemberId);
                role.AddMember(notification.TeamMemberId);
                _roleRepository.Update(role);
            }

            await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}