using System.Text.Json.Serialization;
using Mcm.Shared.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Mcm.Authorizations.Application.Features.Roles.Commands.DeleteRole
{
    public class DeleteRoleCommand
        : IRequest<ApiResponse<DeleteRoleResponse>>
    {
       public Guid Id { get; set; }
       public bool Force { get; set; }
    }
}