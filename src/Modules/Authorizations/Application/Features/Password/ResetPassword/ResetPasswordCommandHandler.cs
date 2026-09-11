using System.Buffers.Text;
using System.Security.Cryptography;
using Mcm.Authorizations.Application.Interfaces;
using Mcm.Authorizations.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;

namespace Mcm.Authorizations.Application.Features.Password.ResetPassword
{
    public class ResetPasswordCommandHandler(ITeamMemberRepository teamMemberRepository, ICurrentUserService currentUserService, IJwtTokenService jwtTokenService, IHashPasswordService passwordService, IAuthorizationUow uow)
        : IRequestHandler<ResetPasswordCommand, ApiResponse<ResetPasswordResponse>>
    {
        private readonly ITeamMemberRepository _teamMemberRepository = teamMemberRepository;
        private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IHashPasswordService _passwordService = passwordService;
        private readonly IAuthorizationUow _uow = uow;

        public async Task<ApiResponse<ResetPasswordResponse>> Handle(ResetPasswordCommand command, CancellationToken ct)
        {
            var teamMemberId = _currentUserService.TeamMemberId;
            var teamMember = await _teamMemberRepository.GetByIdAsync(teamMemberId)
                        ?? throw NotFoundException.NotFoundById(nameof(TeamMember), teamMemberId);

            if (teamMemberId != command.Id)
                throw new UnauthorizedException("Access not authorized to reset password");
        
            teamMember.SetPassword(_passwordService.HashPassword(command.Password));

            _teamMemberRepository.Update(teamMember);
            await _uow.SaveChangesAsync(ct);

            return new ApiResponse<ResetPasswordResponse>
            {
                Success = true,
                Message = "Forgetting Password",
                Code = 200,
                Data = new ResetPasswordResponse{TeamMemberId = teamMember.Id}
            };
        }
    }
}