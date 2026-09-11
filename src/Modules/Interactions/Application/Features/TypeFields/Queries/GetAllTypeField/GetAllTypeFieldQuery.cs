using Mcm.Interactions.Application.Features.InteractionTypes.Queries.GetAllInteractionType;
using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Interactions.Application.Features.TypeFields.Queries.GetAllTypeField
{
    public record GetAllTypeFieldQuery(GetAllTypeFieldRequest Request)
        : IRequest<ApiResponse<List<GetAllTypeFieldResponse>>>;
}