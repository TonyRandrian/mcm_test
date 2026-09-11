using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Authorizations.Application.Features.Roles.Queries.GetAllRole
{
    public record GetAllRoleQuery(GetAllRoleRequest Header)
        : IRequest<ApiResponse<GetAllRoleResponse>>
    {
    }
}