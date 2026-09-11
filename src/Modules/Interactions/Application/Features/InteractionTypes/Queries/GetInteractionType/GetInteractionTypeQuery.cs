using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Interactions.Application.Features.InteractionTypes.Queries.GetInteractionType
{
    public record GetInteractionTypeQuery(GetInteractionTypeRequest Request)
        : IRequest<ApiResponse<GetInteractionTypeResponse>>;
}