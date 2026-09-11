using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Interactions.Application.Features.Interactions.Queries.GetAllInteraction
{
    public record GetAllInteractionQuery(GetAllInteractionRequest Request)
        : IRequest<ApiResponse<List<GetAllInteractionResponse>>>;
}