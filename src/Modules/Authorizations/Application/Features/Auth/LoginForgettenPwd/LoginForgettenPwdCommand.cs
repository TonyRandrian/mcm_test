using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Authorizations.Application.Features.Auth.LoginForgettenPwd
{
    public class LoginForgettenPwdCommand
        : IRequest<ApiResponse<LoginForgettenPwdResponse>>
    {
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}