using Mcm.Shared.Application.Common;

namespace Mcm.Interactions.Application.Features.Interactions.Queries.GetAllInteraction
{
    public record GetAllInteractionRequest
    (
        string? Search,
        Guid? TeamMemberId,
        Guid? ContactId,
        DateTime? StartDateFrom,
        DateTime? StartDateTo,
        DateTime? EndDateFrom,
        DateTime? EndDateTo,
        int Page,
        int Limit
    );
}