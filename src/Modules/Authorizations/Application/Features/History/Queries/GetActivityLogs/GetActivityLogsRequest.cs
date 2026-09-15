namespace Mcm.Authorizations.Application.Features.History.Queries.GetActivityLogs
{
    public record GetActivityLogsRequest
    (
        string? Search,
        string? Category,
        int Page = 1,
        int Limit = 10
    );
}