using Mcm.Catalog.Application.Interfaces;
using Mcm.Catalog.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Enums;
using MediatR;

namespace Mcm.Catalog.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler(IProductRepository productRepository, ICurrentUserService currentUserService, ICurrencyRepository currencyRepository, IProductCategoryRepository productCategoryRepository, IResourceService resourceService, ICatalogUow uow) : IRequestHandler<CreateProductCommand, ApiResponse<CreateProductResponse>>
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IResourceService _resourceService = resourceService;
        private readonly ICurrencyRepository _currencyRepository = currencyRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IProductCategoryRepository _productCategoryRepository = productCategoryRepository;
        private readonly ICatalogUow _uow = uow;

        public async Task<ApiResponse<CreateProductResponse>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var currency = await _currencyRepository.GetByIdAsync(command.CurrencyId)
                ?? throw NotFoundException.NotFoundById(nameof(Currency), command.CurrencyId);

            var product = Product.Create(
                _currentUserService.CompanyId,
                command.Name,
                command.Description,
                command.Price,
                command.Unit,
                currency.Id
            );

            var cp = await _resourceService.SaveResource(command.CoverPicture, FileType.Image);
            product.UpdateCoverPicture(cp);
            if (command.Images is not null)
            {
                foreach (var image in command.Images)
                {
                    var resource = await _resourceService.SaveResource(image, FileType.Image);
                    product.AddImage(resource);
                }
            }

            if (command.CategoryIds is not null)
            {
                foreach (var categoryId in command.CategoryIds)
                {
                    var category = await _productCategoryRepository.GetByIdAsync(categoryId);
                    if (category is null)
                        break;
                    product.AddCategory(category);
                }
            }

            await _productRepository.AddAsync(product);    
            await _uow.SaveChangesAsync(cancellationToken);
            
            return new ApiResponse<CreateProductResponse>
            {
                Success = true,
                Message = "Product created successfully",
                Code = 200,
                Data = new CreateProductResponse{ProductId = product.Id}
            };
        }
    }
}