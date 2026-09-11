using Mcm.Company.Application.Interfaces;
using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Company.Application.Features.ActivitySectors.Queries.GetAllActivitySector
{
    public class GetAllActivitySectorQueryHandler(
        IActivitySectorRepository activitySectorRepository)
        : IRequestHandler<GetAllActivitySectorQuery, ApiResponse<GetAllActivitySectorResponse>>
    {
        private readonly IActivitySectorRepository _activitySectorRepository = activitySectorRepository;
        public async Task<ApiResponse<GetAllActivitySectorResponse>> Handle(GetAllActivitySectorQuery request, CancellationToken cancellationToken)
        {
            var activitySectors = await _activitySectorRepository.GetAllAsync(
                predicate: p => (
                    (string.IsNullOrEmpty(request.Request.SearchName) || p.Name.ToLower().Contains(request.Request.SearchName.ToLower()))),
                orderBy: p => p.OrderBy(p => p.Name)
            );

            return new ApiResponse<GetAllActivitySectorResponse>
            {
                Success = true,
                Message = "Activity sectors retrieved successfully",
                Code = 200,
                Data = new GetAllActivitySectorResponse(activitySectors.Select(res => new ActivitySectorDto(
                    res.Id, res.Name))
                    .ToList())
            };
        }
    }
}