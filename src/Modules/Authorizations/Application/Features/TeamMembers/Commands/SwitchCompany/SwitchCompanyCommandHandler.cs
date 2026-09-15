using Mcm.Authorizations.Application.Interfaces;
using Mcm.Authorizations.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Modules;
using MediatR;

namespace Mcm.Authorizations.Application.Features.TeamMembers.Commands.SwitchCompany
{
    public class SwitchCompanyCommandHandler(
        IRefreshTokenRepository refreshTokenRepository,
        ITeamMemberRepository teamMemberRepository,
        IJwtTokenService jwtTokenService,
        ICompanyModule companyModule,
        IAuthorizationUow uow)
        : IRequestHandler<SwitchCompanyCommand, ApiResponse<SwitchCompanyResponse>>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
        private readonly ITeamMemberRepository _teamMemberRepository = teamMemberRepository;
        private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
        private readonly ICompanyModule _companyModule = companyModule;
        private readonly IAuthorizationUow _uow = uow;

        public async Task<ApiResponse<SwitchCompanyResponse>> Handle(SwitchCompanyCommand request, CancellationToken cancellationToken)
        {
            
            var oldRefreshToken = await _refreshTokenRepository.GetRefreshTokenAsync(request.RefreshToken)
                ?? throw new UnauthorizedException();

            if (oldRefreshToken.IsExpired || oldRefreshToken.IsUsed)
                throw new UnauthorizedException("Invalid RefreshToken");
            oldRefreshToken.MarkAsUsed();

            var teamMember = await _teamMemberRepository.GetByIdAsync(oldRefreshToken.TeamMemberId)
                ?? throw NotFoundException.NotFoundById(nameof(TeamMember), oldRefreshToken.TeamMemberId);
            var company = await _companyModule.GetCompanyById(request.CompanyId)
                ?? throw NotFoundException.NotFoundById("Company", request.CompanyId);
            if (!_teamMemberRepository.IsAuthorized(teamMember.Id, company.Id))
                throw new UnauthorizedException("Unauthorized in Company");

            var token = await _jwtTokenService.GenerateToken(teamMember, request.CompanyId);
            var refreshToken = _jwtTokenService.GenerateRefreshToken();

            await _refreshTokenRepository.AddAsync(
                RefreshToken.Create(refreshToken, teamMember.Id, company.Id));

            await _uow.SaveChangesAsync(cancellationToken);
            return new ApiResponse<SwitchCompanyResponse>
            {
                Code = 200,
                Data = new SwitchCompanyResponse
                {
                    RefreshToken = refreshToken,
                    Token = token
                },
                Message = "Switching company successfully",
                Success = true
            };
        }
    }
}