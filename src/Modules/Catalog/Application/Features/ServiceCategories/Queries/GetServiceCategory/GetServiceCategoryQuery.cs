using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Catalog.Application.Features.ServiceCategories.Queries.GetServiceCategory
{
    public record GetServiceCategoryQuery(GetServiceCategoryRequest Request)
        : IRequest<ApiResponse<GetServiceCategoryResponse>>;
}