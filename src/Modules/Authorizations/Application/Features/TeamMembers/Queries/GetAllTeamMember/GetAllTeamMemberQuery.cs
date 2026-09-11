using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Authorizations.Application.Features.TeamMembers.Queries.GetAllTeamMember
{
    public record GetAllTeamMemberQuery(GetAllTeamMemberRequestHeader Header)
        : IRequest<ApiResponse<GetAllTeamMemberResponse>>
    {
        
    }
}