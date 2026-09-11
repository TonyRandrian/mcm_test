using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Interactions.Application.Features.InteractionTypes.Queries.GetAllInteractionType
{
    public record GetAllInteractionTypeQuery(GetAllInteractionTypeRequest Request)
        : IRequest<ApiResponse<List<GetAllInteractionTypeResponse>>>;
}