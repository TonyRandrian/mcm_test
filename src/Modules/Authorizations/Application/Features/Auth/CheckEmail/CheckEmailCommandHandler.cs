using Mcm.Authorizations.Application.Features.Auth.Login;
using Mcm.Authorizations.Application.Interfaces;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;

namespace Mcm.Authorizations.Application.Features.Auth.CheckEmail
{
    public class CheckEmailCommandHandler(
        ITeamMemberRepository teamMemberRepository,
        IMailService mailService)
        : IRequestHandler<CheckEmailCommand, ApiResponse<CheckEmailResponse>>
    {
        private readonly ITeamMemberRepository _teamMemberRepository = teamMemberRepository;
        private readonly IMailService _mailService = mailService;
       
        public async Task<ApiResponse<CheckEmailResponse>> Handle(CheckEmailCommand command, CancellationToken cancellationToken)
        {
            var user = await _teamMemberRepository.GetByEmailAsync(new Email(command.Email))
                    ?? throw NotFoundException.NotFoundByEmail(command.Email);

            return new ApiResponse<CheckEmailResponse>
            {
                Success = true,
                Message = "Login successful",
                Code = 200,
                Data = new CheckEmailResponse
                {
                    Email = user.Identity.Email,
                }
            };
        }
    }
}