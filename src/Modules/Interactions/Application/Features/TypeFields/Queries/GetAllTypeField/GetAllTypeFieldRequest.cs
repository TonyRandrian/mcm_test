using Mcm.Shared.Application.Common;

namespace Mcm.Interactions.Application.Features.TypeFields.Queries.GetAllTypeField
{
    public record GetAllTypeFieldRequest
    (
        string? Search,
        Guid? InteractionTypeId,
        int Page = 1,
        int Limit = 10
    );
}