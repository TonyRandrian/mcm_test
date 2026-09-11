using Mcm.Catalog.Application.Features.ProductCategories.Queries.GetAllProductCategory;
using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Catalog.Application.Features.ProductCategories.Queries.GetAllProductCategory
{
    public record GetAllProductCategoryQuery(GetAllProductCategoryRequest Request)
        : IRequest<ApiResponse<GetAllProductCategoryResponse>>;
}