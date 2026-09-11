using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Catalog.Application.Features.Products.Queries.GetAllProduct
{
    public record GetAllProductQuery(GetAllProductRequest Request)
        : IRequest<ApiResponse<GetAllProductResponse>>;
}