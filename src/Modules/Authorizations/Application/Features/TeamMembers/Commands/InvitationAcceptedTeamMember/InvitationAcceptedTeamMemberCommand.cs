using System.Text.Json.Serialization;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;

namespace Mcm.Authorizations.Application.Features.TeamMembers.Commands.InvitationAcceptedTeamMember
{
    public class InvitationAcceptedTeamMemberCommand
        : IRequest<ApiResponse<InvitationAcceptedTeamMemberResponse>>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}