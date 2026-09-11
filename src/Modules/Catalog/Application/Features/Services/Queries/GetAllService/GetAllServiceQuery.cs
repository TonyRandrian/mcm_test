using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Catalog.Application.Features.Services.Queries.GetAllService
{
    public record GetAllServiceQuery(GetAllServiceRequest Request)
        : IRequest<ApiResponse<GetAllServiceResponse>>;
}