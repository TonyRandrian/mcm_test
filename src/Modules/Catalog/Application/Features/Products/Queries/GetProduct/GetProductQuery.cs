using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Catalog.Application.Features.Products.Queries.GetProduct
{
    public record GetProductQuery(GetProductRequest Request)
        : IRequest<ApiResponse<GetProductResponse>>;
}