using Mcm.Catalog.Application.Features.ServiceCategories.Queries.GetAllServiceCategory;
using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Catalog.Application.Features.ServiceCategories.Queries.GetAllServiceCategory
{
    public record GetAllServiceCategoryQuery(GetAllServiceCategoryRequest Request)
        : IRequest<ApiResponse<GetAllServiceCategoryResponse>>;
}