using Mcm.Shared.Application.Common;

namespace Mcm.Interactions.Application.Features.InteractionTypes.Queries.GetAllInteractionType
{
    public record GetAllInteractionTypeRequest
    (
        string? Search = null,
        int Page = 1,
        int Limit = 10
    );
}