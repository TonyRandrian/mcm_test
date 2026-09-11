using System.Text.Json.Serialization;
using Mcm.Shared.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Mcm.Authorizations.Application.Features.Roles.Commands.UpdateRole
{
    public class UpdateRoleCommand
        : IRequest<ApiResponse<UpdateRoleResponse>>
    {
        public Guid Id { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public List<Guid>? Companies { get; set; } = [];
        public IEnumerable<(string Module, string Action)>? Permissions { get; set; } = [];
    }
}