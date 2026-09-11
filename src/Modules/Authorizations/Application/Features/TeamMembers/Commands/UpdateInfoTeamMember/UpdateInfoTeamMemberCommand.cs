using System.Text.Json.Serialization;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;

namespace Mcm.Authorizations.Application.Features.TeamMembers.Commands.UpdateInfoTeamMember
{
    public class UpdateInfoTeamMemberCommand : IRequest<ApiResponse<UpdateInfoTeamMemberResponse>>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Position { get; set; }
        // public bool IsLeader { get; set; }
    }
}