using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Authorizations.Application.Features.Auth.CheckEmail
{
    public class CheckEmailCommand
        : IRequest<ApiResponse<CheckEmailResponse>>
    {
        public string Email { get; set; } = string.Empty;
    }
}