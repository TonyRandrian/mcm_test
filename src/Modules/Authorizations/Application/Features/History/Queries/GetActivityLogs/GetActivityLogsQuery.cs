using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Authorizations.Application.Features.History.Queries.GetActivityLogs
{
    public record GetActivityLogsQuery(GetActivityLogsRequest Request)
        : IRequest<ApiResponse<GetActivityLogsResponse>>;
}