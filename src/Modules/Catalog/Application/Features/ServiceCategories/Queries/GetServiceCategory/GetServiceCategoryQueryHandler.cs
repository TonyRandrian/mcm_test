using Mcm.Catalog.Application.Features.Services.Queries.GetService;
using Mcm.Catalog.Application.Interfaces;
using Mcm.Catalog.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using MediatR;

namespace Mcm.Catalog.Application.Features.ServiceCategories.Queries.GetServiceCategory
{
    public class GetServiceCategoryQueryHandler(IServiceCategoryRepository ServiceCategoryRepository)
        : IRequestHandler<GetServiceCategoryQuery, ApiResponse<GetServiceCategoryResponse>>
    {
        private readonly IServiceCategoryRepository _ServiceCategoryRepository = ServiceCategoryRepository;

        public async Task<ApiResponse<GetServiceCategoryResponse>> Handle(GetServiceCategoryQuery query, CancellationToken cancellationToken)
        {
            var serviceCategory = await _ServiceCategoryRepository.GetByIdAsync(query.Request.Id)
                ?? throw NotFoundException.NotFoundById(nameof(ServiceCategory), query.Request.Id);

            return new ApiResponse<GetServiceCategoryResponse>
            {
                Success = true,
                Message = "Service category retrieved successfully",
                Code = 200,
                Data = new GetServiceCategoryResponse
                {
                    Id = serviceCategory.Id,
                    Name = serviceCategory.Name,
                    Count = serviceCategory.Services.Count
                }
            };
        }
    }
}