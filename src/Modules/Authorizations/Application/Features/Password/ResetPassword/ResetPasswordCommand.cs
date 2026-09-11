using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Authorizations.Application.Features.Password.ResetPassword
{
    public class ResetPasswordCommand
        : IRequest<ApiResponse<ResetPasswordResponse>>
    {
        public Guid Id { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}