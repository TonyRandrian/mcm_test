using Mcm.Catalog.Application.Features.Products.Queries.GetAllProduct;
using Mcm.Catalog.Application.Interfaces;
using Mcm.Property.Domain.Enums;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Interfaces;
using MediatR;

namespace Mcm.Catalog.Application.Features.ProductCategories.Queries.GetAllProductCategory
{
    public class GetAllProductCategoryQueryHandler(IProductCategoryRepository productCategoryRepository)
        : IRequestHandler<GetAllProductCategoryQuery, ApiResponse<GetAllProductCategoryResponse>>
    {
        private readonly IProductCategoryRepository _productCategoryRepository = productCategoryRepository;
       
        public async Task<ApiResponse<GetAllProductCategoryResponse>> Handle(GetAllProductCategoryQuery query, CancellationToken cancellationToken)
        {
            var categories = await _productCategoryRepository.GetAllAsync(
                predicate: p => (
                    (string.IsNullOrEmpty(query.Request.searchName) || p.Name.Contains(query.Request.searchName))),
                orderBy: p => p.OrderDescending(),
                pageQuery: new PageQuery(query.Request.Page, query.Request.Limit));
            
            var tree = categories.ToDictionary(
                x => x.Id,
                x => new ProductCategories
                {
                    Id = x.Id,
                    Name = x.Name,
                    Count = x.ProductRelations.Count
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
                .Where(x => x.ParentCategoryId is null)
                .Select(x => tree[x.Id])
                .ToList();

            return new ApiResponse<GetAllProductCategoryResponse>
            {
                Success = true,
                Message = "Product Categories retrieved successfully",
                Code = 200,
                Data = new GetAllProductCategoryResponse(data),
                Meta = new Meta
                {
                    Page = query.Request.Page,
                    Limit = query.Request.Limit,
                    Total = await _productCategoryRepository.CountAsync()
                }
            };
        }
    }
}