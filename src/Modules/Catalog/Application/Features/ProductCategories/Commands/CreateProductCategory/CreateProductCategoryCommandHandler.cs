using Mcm.Catalog.Application.Interfaces;
using Mcm.Catalog.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Enums;
using MediatR;

namespace Mcm.Catalog.Application.Features.ProductCategories.Commands.CreateProductCategory
{
    public class CreateProductCategoryCommandHandler(IProductRepository productRepository, IProductCategoryRepository productCategoryRepository, IResourceService resourceService, ICatalogUow uow)
    : IRequestHandler<CreateProductCategoryCommand, ApiResponse<CreateProductCategoryResponse>>
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IResourceService _resourceService = resourceService;
        private readonly IProductCategoryRepository _productCategoryRepository = productCategoryRepository;
        private readonly ICatalogUow _uow = uow;

        public async Task<ApiResponse<CreateProductCategoryResponse>> Handle(CreateProductCategoryCommand command, CancellationToken cancellationToken)
        {
            var productCategory = ProductCategory.Create(
                command.ParentCategoryId,
                command.Name
            );

            await _productCategoryRepository.AddAsync(productCategory);   
            await _uow.SaveChangesAsync();
            
                return new ApiResponse<CreateProductCategoryResponse>
                {
                    Success = true,
                Message = "Product created successfully",
                Code = 200,
                Data = new CreateProductCategoryResponse{ProductCategoryId = productCategory.Id}
            };
        }
    }
}