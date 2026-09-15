using Mcm.Shared.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Mcm.Authorizations.Application.Features.Roles.Commands.CreateRole
{
    public class CreateRoleCommand
        : IRequest<ApiResponse<CreateRoleResponse>>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Guid> Companies { get; set; } = [];
        public IEnumerable<CreateRole_Permission> Permissions { get; set; } = [];
    }
}