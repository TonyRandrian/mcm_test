using Mcm.Authorizations.Application.Interfaces;
using Mcm.Authorizations.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;

namespace Mcm.Authorizations.Application.Features.TeamMembers.Commands.UpdateInfoTeamMember
{
    public class UpdateInfoTeamMemberCommandHandler(ITeamMemberRepository teamMemberRepository, ICurrentUserService jwtTokenService,IAuthorizationUow uow)
        : IRequestHandler<UpdateInfoTeamMemberCommand, ApiResponse<UpdateInfoTeamMemberResponse>>
    {
        public ITeamMemberRepository _teamMemberRepository = teamMemberRepository;
        public ICurrentUserService _jwtTokenService = jwtTokenService;
        public IAuthorizationUow _uow = uow;

        public async Task<ApiResponse<UpdateInfoTeamMemberResponse>> Handle(UpdateInfoTeamMemberCommand command, CancellationToken cancellationToken)
        {
            var tmId = _jwtTokenService.TeamMemberId;
            var teamMember = await _teamMemberRepository.GetByIdAsync(tmId)
                ?? throw NotFoundException.NotFoundById(nameof(TeamMember), tmId); 
        
            teamMember.Update(new Identity(
                command.LastName, command.FirstName, teamMember.Identity.Email, teamMember.Identity.Position
            ));

            _teamMemberRepository.Update(teamMember);      
            await _uow.SaveChangesAsync(cancellationToken);

            return new ApiResponse<UpdateInfoTeamMemberResponse>
            {
                Success = true,
                Message = "PUT UpdateInfoTeamMember",
                Code = 200,
                Data = new UpdateInfoTeamMemberResponse{ TeamMemberId = teamMember.Id }
            };
        }
    }
}