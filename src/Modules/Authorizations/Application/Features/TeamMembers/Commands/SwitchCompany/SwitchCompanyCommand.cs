using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Authorizations.Application.Features.TeamMembers.Commands.SwitchCompany
{
    public class SwitchCompanyCommand
        : IRequest<ApiResponse<SwitchCompanyResponse>>
    {
        public string RefreshToken { get; set; } = string.Empty;
        public Guid CompanyId { get; set; }
    }
}