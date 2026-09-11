using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Company.Application.Features.ActivitySectors.Queries.GetAllActivitySector
{
    public record GetAllActivitySectorQuery(GetAllActivitySectorRequest Request)
        : IRequest<ApiResponse<GetAllActivitySectorResponse>>;
}