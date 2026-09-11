using Mcm.Authorizations.Application.Interfaces;
using Mcm.Authorizations.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using MediatR;

namespace Mcm.Authorizations.Application.Features.TeamMembers.Commands.InvitationDeniedTeamMember
{
    public class InvitationDeniedTeamMemberCommandHandler(ITeamMemberRepository teamMemberRepository, IHashPasswordService passwordService, IJwtTokenService jwtTokenService, IAuthorizationUow uow)
        : IRequestHandler<InvitationDeniedTeamMemberCommand, ApiResponse<InvitationDeniedTeamMemberResponse>>
    {
        private readonly ITeamMemberRepository _teamMemberRepository = teamMemberRepository;
        private readonly IHashPasswordService _passwordService = passwordService;
        private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
        private readonly IAuthorizationUow _uow = uow;

        public async Task<ApiResponse<InvitationDeniedTeamMemberResponse>> Handle(InvitationDeniedTeamMemberCommand command, CancellationToken ct)
        {
            var objectToken = _jwtTokenService.VerifyToken(command.AccessToken);
            var teamMember = await _teamMemberRepository.GetByIdOutTenantAsync(objectToken.TeamMemberId)
                        ?? throw NotFoundException.NotFoundById(nameof(TeamMember), objectToken.TeamMemberId);
            
            _teamMemberRepository.HardDelete(teamMember);
            await _uow.SaveChangesAsync(ct);
            
            return new ApiResponse<InvitationDeniedTeamMemberResponse>
            {
                Success = true,
                Message = "Invitation denied successfully",
                Code = 200,
                Data = new InvitationDeniedTeamMemberResponse{Unit = Unit.Value}
            };
        }
    }
}