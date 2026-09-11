using Mcm.Authorizations.Application.Interfaces;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;

namespace Mcm.Authorizations.Application.Features.Auth.Login
{
    public class LoginCommandHandler(ITeamMemberRepository teamMemberRepository, IAuthorizationUow uow, IHashPasswordService hashPasswordService, IJwtTokenService jwtTokenService) : IRequestHandler<LoginCommand, ApiResponse<LoginResponse>>
    {
        private readonly ITeamMemberRepository _teamMemberRepository = teamMemberRepository;
        private readonly IAuthorizationUow _uow = uow;
        private readonly IHashPasswordService _hashPasswordService = hashPasswordService;
        private readonly IJwtTokenService _jwtTokenService = jwtTokenService;

        public async Task<ApiResponse<LoginResponse>> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var user = await _teamMemberRepository.GetByEmailAsync(new Email(command.Email))
                    ?? throw NotFoundException.NotFoundByEmail(command.Email);

            if (! _hashPasswordService.VerifyPassword(user.HashedPassword, command.Password))
                throw new BadRequestException("Invalid password");

            user.Connect();
            _teamMemberRepository.Update(user);
            
            await _uow.SaveChangesAsync(cancellationToken);

            return new ApiResponse<LoginResponse>
            {
                Success = true,
                Message = "Login successful",
                Code = 200,
                Data = new LoginResponse
                {
                    Token = await _jwtTokenService.GenerateToken(user),
                    CompanyId = user.CompanyId
                }
            };
        }
    }
}