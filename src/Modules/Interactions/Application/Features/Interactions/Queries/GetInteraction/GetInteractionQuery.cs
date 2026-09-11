using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Interactions.Application.Features.Interactions.Queries.GetInteraction
{
    public record GetInteractionQuery(GetInteractionRequest Request)
        : IRequest<ApiResponse<GetInteractionResponse>>;
}