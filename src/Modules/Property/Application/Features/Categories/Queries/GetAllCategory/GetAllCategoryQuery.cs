using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Property.Application.Features.Categories.Queries.GetAllCategory
{
    public record GetAllCategoryQuery(GetAllCategoriesRequest Request)
        : IRequest<ApiResponse<GetAllCategoryResponse>>;
}