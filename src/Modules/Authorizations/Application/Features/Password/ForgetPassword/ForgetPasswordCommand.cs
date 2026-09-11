using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Authorizations.Application.Features.Password.ForgetPassword
{
    public class ForgetPasswordCommand 
        : IRequest<ApiResponse<ForgetPasswordResponse>>
    {
        public string Email { get; set; } = string.Empty;
    }
}