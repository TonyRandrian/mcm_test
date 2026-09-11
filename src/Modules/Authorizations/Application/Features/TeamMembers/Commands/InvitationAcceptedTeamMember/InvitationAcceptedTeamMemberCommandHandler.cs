using Mcm.Authorizations.Application.Interfaces;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;

namespace Mcm.Authorizations.Application.Features.TeamMembers.Commands.InvitationAcceptedTeamMember
{
    public class InvitationAcceptedTeamMemberCommandHandler(ITeamMemberRepository teamMemberRepository, IHashPasswordService passwordService, IJwtTokenService jwtTokenService, IAuthorizationUow uow)
        : IRequestHandler<InvitationAcceptedTeamMemberCommand, ApiResponse<InvitationAcceptedTeamMemberResponse>>
    {
        private readonly ITeamMemberRepository _teamMemberRepository = teamMemberRepository;
        private readonly IHashPasswordService _passwordService = passwordService;
        private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
        private readonly IAuthorizationUow _uow = uow;

        public async Task<ApiResponse<InvitationAcceptedTeamMemberResponse>> Handle(InvitationAcceptedTeamMemberCommand command, CancellationToken cancellationToken)
        {
            var objectToken = _jwtTokenService.VerifyToken(command.AccessToken);
            var teamMember = await _teamMemberRepository.GetByIdOutTenantAsync(objectToken.TeamMemberId)
                        ?? throw NotFoundException.NotFoundById(nameof(Authorizations.Domain.Entities.TeamMember), objectToken.TeamMemberId);
            
            teamMember.Update(
                new Identity(command.LastName, command.FirstName, command.Email, string.Empty));
            teamMember.SetPassword(_passwordService.HashPassword(command.Password));
            teamMember.InvitationAccepted();
            
            // _teamMemberRepository.Update(teamMember);
            await _uow.SaveChangesAsync(cancellationToken);
            
            return new ApiResponse<InvitationAcceptedTeamMemberResponse>
            {
                Success = true,
                Message = "Invitation TeamMember Accepted",
                Code = 200,
                Data = new InvitationAcceptedTeamMemberResponse{Id = teamMember.Id}
            };
        }
    }
}