using System.Text.Json;
using Mcm.Authorizations.Application.Interfaces;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Interfaces;
using MediatR;

namespace Mcm.Authorizations.Application.Features.TeamMembers.Queries.GetAllTeamMember
{
    public class GetAllTeamMemberQueryHandler(ITeamMemberRepository teamMemberRepository, ICurrentUserService currentUserService)
        : IRequestHandler<GetAllTeamMemberQuery, ApiResponse<GetAllTeamMemberResponse>>
    {
        private readonly ITeamMemberRepository _teamMemberRepository = teamMemberRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<ApiResponse<GetAllTeamMemberResponse>> Handle(GetAllTeamMemberQuery query, CancellationToken cancellationToken)
        {
            var teamMembers = await _teamMemberRepository.GetAllAsync(
                predicate: tm => tm.CompanyId == _currentUserService.CompanyId,
                orderBy: tm => tm.OrderByDescending(e => e.CreatedAt),
                pageQuery: new PageQuery(query.Header.Page, query.Header.Limit));
            
            return new ApiResponse<GetAllTeamMemberResponse>
            {
                Success = true,
                Message = "TeamMembers get successfully",
                Code = 200,
                Data = new GetAllTeamMemberResponse
                {
                    TeamMembers = [.. teamMembers.Select(teamMember => new TeamMemberResponse
                    {
                        Id = teamMember.Id,
                        LastName = teamMember.Identity.LastName,
                        FirstName = teamMember.Identity.FirstName,
                        Role = teamMember.Roles.Count() > 0 ? teamMember.Roles[0].Role.Title.Value : "Utilisateur"
                    })]
                },
                Meta = new Meta
                {
                    Page = query.Header.Page,
                    Limit = query.Header.Limit,
                    Total = await _teamMemberRepository.CountAsync()
                }
            };
        }
    }
}