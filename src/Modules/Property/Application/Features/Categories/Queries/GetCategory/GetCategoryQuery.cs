using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Property.Application.Features.Categories.Queries.GetCategory
{
    public record GetCategoryQuery(GetCategoryRequest Request)
        : IRequest<ApiResponse<GetCategoryResponse>>;
}