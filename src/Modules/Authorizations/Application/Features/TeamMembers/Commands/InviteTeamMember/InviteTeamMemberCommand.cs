using System.Text.Json.Serialization;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;

namespace Mcm.Authorizations.Application.Features.TeamMembers.Commands.InviteTeamMember
{
    public class InviteTeamMemberCommand : IRequest<ApiResponse<InviteTeamMemberResponse>>
    {
        public Guid CompanyId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; } = string.Empty;
        public Guid? RoleId { get; set; }
        public bool IsLeader { get; set; }
    }
}