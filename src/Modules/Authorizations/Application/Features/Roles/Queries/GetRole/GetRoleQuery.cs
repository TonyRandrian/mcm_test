using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Authorizations.Application.Features.Roles.Queries.GetRole
{
    public record GetRoleQuery(GetRoleRequest Header)
        : IRequest<ApiResponse<GetRoleResponse>>;
}