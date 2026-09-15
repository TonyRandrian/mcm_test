using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Authorizations.Application.Features.Auth.RefreshToken
{
    public class RefreshTokenCommand
        : IRequest<ApiResponse<RefreshTokenResponse>>
    {
        public string RefreshToken { get; set; } = string.Empty;    
    }
}