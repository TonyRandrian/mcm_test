using Mcm.Authorizations.Application.Interfaces;
using Mcm.Authorizations.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;

namespace Mcm.Authorizations.Application.Features.Auth.RefreshToken
{
    public class RefreshTokenCommandHandler(
        IJwtTokenService jwtTokenService,
        IRefreshTokenRepository refreshTokenRepository,
        ITeamMemberRepository teamMemberRepository,
        IAuthorizationUow uow)
        : IRequestHandler<RefreshTokenCommand, ApiResponse<RefreshTokenResponse>>
    {
        private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
        private readonly ITeamMemberRepository _teamMemberRepository = teamMemberRepository;
        private readonly IAuthorizationUow _uow = uow;
        public async Task<ApiResponse<RefreshTokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var oldRefreshToken = await _refreshTokenRepository.GetRefreshTokenAsync(request.RefreshToken)
                ?? throw new UnauthorizedException();

            if (oldRefreshToken.IsExpired || oldRefreshToken.IsUsed)
                throw new UnauthorizedException();
            oldRefreshToken.MarkAsUsed();

            var teamMember = await _teamMemberRepository.GetByIdOutTenantAsync(oldRefreshToken.TeamMemberId)
                ?? throw NotFoundException.NotFoundById(nameof(TeamMember), oldRefreshToken.TeamMemberId);

            var token = await _jwtTokenService.GenerateToken(teamMember, oldRefreshToken.CompanyId);
            var refreshToken = _jwtTokenService.GenerateRefreshToken();

            await _refreshTokenRepository.AddAsync(
                Domain.Entities.RefreshToken.Create(refreshToken, teamMember.Id, oldRefreshToken.CompanyId));

            await _uow.SaveChangesAsync(cancellationToken);
            return new ApiResponse<RefreshTokenResponse>
            {
                Code = 200,
                Data = new RefreshTokenResponse
                {
                    RefreshToken = refreshToken,
                    Token = token
                },
                Message = "Refresh token successfully",
                Success = true
            };
        }
    }
}