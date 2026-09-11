using Mcm.Authorizations.Application.Interfaces;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;

namespace Mcm.Authorizations.Application.Features.Auth.LoginForgettenPwd;

public class LoginForgettenPwdCommandHandler(ITeamMemberRepository teamMemberRepository, IAuthorizationUow uow, IHashPasswordService hashPasswordService, IPwdResetRepository resetRepository, IJwtTokenService jwtTokenService)
    : IRequestHandler<LoginForgettenPwdCommand, ApiResponse<LoginForgettenPwdResponse>>
{
    private readonly ITeamMemberRepository _teamMemberRepository = teamMemberRepository;
    private readonly IAuthorizationUow _uow = uow;
    private readonly IHashPasswordService _hashPasswordService = hashPasswordService;
    private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
    private readonly IPwdResetRepository _resetRepository = resetRepository;

    public async Task<ApiResponse<LoginForgettenPwdResponse>> Handle(LoginForgettenPwdCommand command, CancellationToken ct)
    {
        var user = await _teamMemberRepository.GetByEmailAsync(new Email(command.Email))
                ?? throw NotFoundException.NotFoundByEmail(command.Email);

        var pwdReset = await _resetRepository.Validate(e => e.TeamMemberId == user.Id && !e.IsUsed)
                ?? throw new NotFoundException("not found password token");

        if (! _hashPasswordService.VerifyPassword(pwdReset.TokenHashed, command.Token)
            || pwdReset.ExpiredAt < DateTime.UtcNow)
            throw new BadRequestException("Invalid password token");

        pwdReset.MakeUsed();
        user.Connect();
        _teamMemberRepository.Update(user);
        
        await _uow.SaveChangesAsync(ct);

        return new ApiResponse<LoginForgettenPwdResponse>
        {
            Success = true,
            Message = "Login successful",
            Code = 200,
            Data = new LoginForgettenPwdResponse
            {
                Token = await _jwtTokenService.GenerateToken(user)
            }
        };
    }
}