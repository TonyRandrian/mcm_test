using Mcm.Catalog.Application.Interfaces;
using Mcm.Catalog.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using MediatR;

namespace Mcm.Catalog.Application.Features.Products.Queries.GetProduct
{
    public class GetProductQueryHandler(IProductRepository productRepository)
        : IRequestHandler<GetProductQuery, ApiResponse<GetProductResponse>>
    {
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<ApiResponse<GetProductResponse>> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(query.Request.Id)
                ?? throw NotFoundException.NotFoundById(nameof(Product), query.Request.Id);

            return new ApiResponse<GetProductResponse>
            {
                Success = true,
                Message = "Product retrieved successfully",
                Code = 200,
                Data = new GetProductResponse
                {
                    Id = product.Id,
                    Name =  product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Unit = product.Unit,
                    CoverPicture = new ResourceResponse(
                        product.CoverPicture.FileType.ToString(),
                        product.CoverPicture.Url,
                        product.CoverPicture.AlternativeText,
                        product.CoverPicture.StorageType.ToString()
                    ),
                    Images = product.Images.Select(img => new ResourceResponse(
                        img.FileType.ToString(),
                        img.Url,
                        img.AlternativeText,
                        img.StorageType.ToString()
                    )).ToList(),
                    Currency = new ProductCurrency
                    {
                        Id = product.CurrencyId,
                        Name = product.Currency.Name,
                        Symbol = product.Currency.Symbol
                    },
                    Categories = [.. product.CategoryRelations.Select(cr => 
                        new ProductCategoryResponse { Id = cr.CategoryId, Name = cr.Category.Name })]
                }         
            };
        }
    }
}