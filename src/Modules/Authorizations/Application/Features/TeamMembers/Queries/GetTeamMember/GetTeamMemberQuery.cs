using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Authorizations.Application.Features.TeamMembers.Queries.GetTeamMember
{
    public record GetTeamMemberQuery(GetTeamMemberRequest Header)
        : IRequest<ApiResponse<GetTeamMemberResponse>>;
}