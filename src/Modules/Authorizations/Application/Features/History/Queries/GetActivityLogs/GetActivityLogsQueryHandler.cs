using Mcm.Authorizations.Application.Interfaces;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using MediatR;

namespace Mcm.Authorizations.Application.Features.History.Queries.GetActivityLogs
{
    public class GetActivityLogsQueryHandler(
        ICurrentUserService currentUserService,
        IActivityLogRepository activityLogRepository,
        ITeamMemberModule teamMemberModule)
        : IRequestHandler<GetActivityLogsQuery, ApiResponse<GetActivityLogsResponse>>
    {
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IActivityLogRepository _activityLogRepository = activityLogRepository;
        private readonly ITeamMemberModule _teamMemberModule = teamMemberModule;
        public async Task<ApiResponse<GetActivityLogsResponse>> Handle(GetActivityLogsQuery request, CancellationToken cancellationToken)
        {
            var query = request.Request;
            var logs = (await _activityLogRepository.GetAllAsync(
                predicate: l =>
                    (string.IsNullOrEmpty(query.Search) ||
                        l.EventType.ToLower().Contains(query.Search.ToLower()) ||
                        l.PayloadJson.ToLower().Contains(query.Search.ToLower())) &&
                    l.CompanyId == _currentUserService.CompanyId,
                orderBy: q => q.OrderByDescending(l => l.OccuredAt),
                ct: cancellationToken
            )).ToList();

            var filtered = logs
                .Select(log =>
                {
                    var (category, label) = EventDescriptor.Describe(log.EventType);
                    return new ActivityLogResponse
                    {
                        Id = log.Id,
                        EventType = log.EventType,
                        Category = category,
                        Label = label,
                        Detail = EventDescriptor.Detail(log.EventType, log.PayloadJson),
                        Author =
                        {
                            Id = log.AuthorId
                        },
                        OccuredAt = log.OccuredAt,
                    };
                })
                .Where(log => string.IsNullOrEmpty(query.Category)
                    || log.Category == query.Category)
                .ToList();

            var authorIds = filtered
                .Where(l => l.Author.Id.HasValue)
                .Select(l => l.Author.Id!.Value)
                .Distinct()
                .ToList();

            if (authorIds.Count > 0)
            {
                var tms = await _teamMemberModule.GetIdentities(authorIds);
                var identityMap = tms.ToDictionary(i => i.Id, i => $"{i.FirstName} {i.LastName}");

                foreach (var item in filtered)
                {
                    if (item.Author.Id.HasValue && identityMap.TryGetValue(item.Author.Id.Value, out var fullName))
                        item.Author.FullName = fullName;
                }
            }

            var paged = filtered.Skip((query.Page - 1) * query.Limit)
                .Take(query.Limit)
                .ToList();

            return new ApiResponse<GetActivityLogsResponse>
            {
                Code = 200,
                Message = "History retrieved successfully",
                Data = new GetActivityLogsResponse{ Logs = paged },
                Success = true,
                Meta = new Meta
                {
                    Page = query.Page,
                    Limit = query.Limit,
                    Total = filtered.Count
                }
            };
        }
    }
}