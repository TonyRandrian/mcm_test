using System.Text.Json.Serialization;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;

namespace Mcm.Authorizations.Application.Features.TeamMembers.Commands.InvitationDeniedTeamMember
{
    public class InvitationDeniedTeamMemberCommand
        : IRequest<ApiResponse<InvitationDeniedTeamMemberResponse>>
    {
        public string AccessToken { get; set; } = null!;
    }
}