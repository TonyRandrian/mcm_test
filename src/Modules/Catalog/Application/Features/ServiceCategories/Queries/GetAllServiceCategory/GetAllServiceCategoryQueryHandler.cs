using Mcm.Catalog.Application.Features.Services.Queries.GetAllService;
using Mcm.Catalog.Application.Interfaces;
using Mcm.Property.Domain.Enums;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Interfaces;
using MediatR;

namespace Mcm.Catalog.Application.Features.ServiceCategories.Queries.GetAllServiceCategory
{
    public class GetAllServiceCategoryQueryHandler(IServiceCategoryRepository ServiceCategoryRepository)
        : IRequestHandler<GetAllServiceCategoryQuery, ApiResponse<GetAllServiceCategoryResponse>>
    {
        private readonly IServiceCategoryRepository _ServiceCategoryRepository = ServiceCategoryRepository;
       
        public async Task<ApiResponse<GetAllServiceCategoryResponse>> Handle(GetAllServiceCategoryQuery query, CancellationToken cancellationToken)
        {
            var categories = await _ServiceCategoryRepository.GetAllAsync(
                predicate: p => (
                    (string.IsNullOrEmpty(query.Request.searchName) || p.Name.Contains(query.Request.searchName))),
                orderBy: p => p.OrderDescending(),
                pageQuery: new PageQuery(query.Request.Page, query.Request.Limit));
            
            
            var tree = categories.ToDictionary(
                x => x.Id,
                x => new ServiceCategoryResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Count = x.Services.Count
                }
            );
            foreach (var category in categories)
            {
                if (category.ParentCategoryId.HasValue &&
                    tree.TryGetValue(category.ParentCategoryId.Value, out var parent))
                {
                    parent.SubCategories.Add(tree[category.Id]);
                }
            }

            var data = categories
                .Where(x => x.ParentCategoryId == null)
                .Select(x => tree[x.Id])
                .ToList();

            return new ApiResponse<GetAllServiceCategoryResponse>
            {
                Success = true,
                Message = "Service Categories retrieved successfully",
                Code = 200,
                Data = new GetAllServiceCategoryResponse(data),
                Meta = new Meta
                {
                    Page = query.Request.Page,
                    Limit = query.Request.Limit,
                    Total = await _ServiceCategoryRepository.CountAsync()
                }
            };
        }
    }
}