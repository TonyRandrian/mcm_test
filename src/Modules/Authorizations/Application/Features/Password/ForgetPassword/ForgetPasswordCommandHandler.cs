using System.Buffers.Text;
using System.Security.Cryptography;
using Mcm.Authorizations.Application.Interfaces;
using Mcm.Authorizations.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;

namespace Mcm.Authorizations.Application.Features.Password.ForgetPassword
{
    public class ForgetPasswordCommandHandler(ITeamMemberRepository teamMemberRepository, IPwdResetRepository resetRepository, IHashPasswordService passwordService, IAuthorizationUow uow)
        : IRequestHandler<ForgetPasswordCommand, ApiResponse<ForgetPasswordResponse>>
    {
        private readonly ITeamMemberRepository _teamMemberRepository = teamMemberRepository;
        private readonly IPwdResetRepository _resetRepository = resetRepository;
        private readonly IHashPasswordService _passwordService = passwordService;
        private readonly IAuthorizationUow _uow = uow;

        public async Task<ApiResponse<ForgetPasswordResponse>> Handle(ForgetPasswordCommand command, CancellationToken ct)
        {
            
            var teamMember = await _teamMemberRepository.GetByEmailAsync(command.Email)
                        ?? throw NotFoundException.NotFoundByEmail(command.Email);

            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
            PasswordReset pwdReset = PasswordReset.Create(teamMember.Id, _passwordService.HashPassword(token));
            
            await _resetRepository.AddAsync(pwdReset);
            await _uow.SaveChangesAsync(ct);
            return new ApiResponse<ForgetPasswordResponse>
            {
                Success = true,
                Message = "Forgetting Password",
                Code = 200,
                Data = new ForgetPasswordResponse{TeamMemberId = teamMember.Id}
            };
        }
    }
}