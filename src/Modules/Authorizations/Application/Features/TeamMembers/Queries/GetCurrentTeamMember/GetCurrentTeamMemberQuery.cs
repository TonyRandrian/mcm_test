using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Authorizations.Application.Features.TeamMembers.Queries.GetCurrentTeamMember
{
    public record GetCurrentTeamMemberQuery()
        : IRequest<ApiResponse<GetCurrentTeamMemberResponse>>;
}