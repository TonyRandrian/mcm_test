using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Catalog.Application.Features.Services.Queries.GetService
{
    public record GetServiceQuery(GetServiceRequest Request)
        : IRequest<ApiResponse<GetServiceResponse>>;
}