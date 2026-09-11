using Mcm.Catalog.Application.Features.Products.Queries.GetProduct;
using Mcm.Catalog.Application.Interfaces;
using Mcm.Catalog.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using MediatR;

namespace Mcm.Catalog.Application.Features.ProductCategories.Queries.GetProductCategory
{
    public class GetProductCategoryQueryHandler(IProductCategoryRepository productCategoryRepository)
        : IRequestHandler<GetProductCategoryQuery, ApiResponse<GetProductCategoryResponse>>
    {
        private readonly IProductCategoryRepository _productCategoryRepository = productCategoryRepository;

        public async Task<ApiResponse<GetProductCategoryResponse>> Handle(GetProductCategoryQuery query, CancellationToken cancellationToken)
        {
            var productCategory = await _productCategoryRepository.GetByIdAsync(query.Request.Id)
                ?? throw NotFoundException.NotFoundById(nameof(ProductCategory), query.Request.Id);

            return new ApiResponse<GetProductCategoryResponse>
            {
                Success = true,
                Message = "Product category retrieved successfully",
                Code = 200,
                Data = new GetProductCategoryResponse
                {
                    Id = productCategory.Id,
                    Name = productCategory.Name,
                    Count = productCategory.ProductRelations.Count
                }
            };
        }
    }
}