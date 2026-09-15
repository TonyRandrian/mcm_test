using Mcm.Catalog.Application.Interfaces;
using Mcm.Property.Domain.Enums;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Interfaces;
using MediatR;

namespace Mcm.Catalog.Application.Features.Products.Queries.GetAllProduct
{
    public class GetAllProductQueryHandler(
        IProductRepository productRepository,
        ICurrentUserService currentUserService)
        : IRequestHandler<GetAllProductQuery, ApiResponse<GetAllProductResponse>>
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        
        public async Task<ApiResponse<GetAllProductResponse>> Handle(GetAllProductQuery query, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync(
                includes: [
                    p => p.Currency,
                    p => p.CategoryRelations
                ],
                predicate: p => 
                    (string.IsNullOrEmpty(query.Request.searchName) || 
                    p.Name.Contains(query.Request.searchName)) &&
                    p.CompanyId == _currentUserService.CompanyId,
                orderBy: p => p.OrderBy(op => op.Name),
                pageQuery: new PageQuery(query.Request.Page, query.Request.Limit), 
                ct: cancellationToken);
            
            var data = products.Select(res => new ProductResponse
            {
                Id = res.Id,
                Name = res.Name,
                Description = res.Description,
                Price = res.Price,
                Unit = res.Unit,
                CoverPicture = new ResourceResponse(
                    res.CoverPicture.FileType.ToString(),
                    res.CoverPicture.Url,
                    res.CoverPicture.AlternativeText,
                    res.CoverPicture.StorageType.ToString()
                ),
                Currency = new ProductCurrencyResponse
                {
                    Id = res.CurrencyId,
                    Name = res.Currency.Name,
                    Symbol = res.Currency.Symbol
                },
                Images = res.Images?.Select(img => new ResourceResponse(
                    img.FileType.ToString(),
                    img.Url,
                    img.AlternativeText,
                    img.StorageType.ToString()
                )).ToList() ?? new List<ResourceResponse>(),
                Categories = res.CategoryRelations?
                    .Select(cr => new ProductCategoriesResponse { Id = cr.CategoryId, Name = cr.Category.Name }).ToList(),
    
            }).ToList();

            return new ApiResponse<GetAllProductResponse>
            {
                Success = true,
                Message = "Products retrieved successfully",
                Code = 200,
                Data = new GetAllProductResponse(data),
                Meta = new Meta
                {
                    Page = query.Request.Page,
                    Limit = query.Request.Limit,
                    Total = await _productRepository.CountAsync()
                }
            };
        }
    }
}