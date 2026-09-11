using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Catalog.Application.Features.ProductCategories.Queries.GetProductCategory
{
    public record GetProductCategoryQuery(GetProductCategoryRequest Request)
        : IRequest<ApiResponse<GetProductCategoryResponse>>;
}