using Mcm.Shared.Application.Common;

namespace Mcm.Interactions.Application.Features.Reports.Queries.GetAllReport
{
    public record GetAllReportRequest
    (
        string? Search,
        int Page = 1,
        int Limit = 10
    );
}