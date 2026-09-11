using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Interactions.Application.Features.TypeFields.Queries.GetTypeField
{
    public record GetTypeFieldQuery(GetTypeFieldRequest Request)
        : IRequest<ApiResponse<GetTypeFieldResponse>>;
}