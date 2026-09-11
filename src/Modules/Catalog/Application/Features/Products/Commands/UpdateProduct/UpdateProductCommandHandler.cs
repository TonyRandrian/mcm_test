using Mcm.Catalog.Application.Features.Products.Commands.UpdateProduct;
using Mcm.Catalog.Application.Interfaces;
using Mcm.Catalog.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Enums;
using MediatR;

namespace Mcm.Catalog.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler(IProductRepository productRepository, ICurrencyRepository currencyRepository, IProductCategoryRepository categoryRepository, IResourceService resourceService, ICatalogUow uow) : IRequestHandler<UpdateProductCommand, ApiResponse<UpdateProductResponse>>
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly ICurrencyRepository currencyRepository = currencyRepository;
        private readonly IProductCategoryRepository _categoryRepository = categoryRepository;
        private readonly IResourceService _resourceService = resourceService;
        private readonly ICatalogUow _uow = uow;
        public async Task<ApiResponse<UpdateProductResponse>> Handle(UpdateProductCommand command, CancellationToken ct)
        {
            var product = await _productRepository.GetByIdAsync(command.Id)
                    ?? throw NotFoundException.NotFoundById(nameof(Product), command.Id);
            var currency = await currencyRepository.GetByIdAsync(command.CurrencyId)
                ?? throw NotFoundException.NotFoundById(nameof(Currency), command.CurrencyId);
            product.Update(
                command.Name,
                command.Description,
                command.Price,
                command.Unit,
                currency.Id
            );
            
            if(command.CoverPicture is not null)
            {
                if (product.CoverPicture is not null)
                    _resourceService.DeleteResource(product.CoverPicture);
                var coverPicture = await _resourceService.SaveResource(command.CoverPicture, FileType.Image);
                product.UpdateCoverPicture(coverPicture);
            }
        
            if (command.Images is not null)
            {
                var toRemove = product.Images?
                    .Where(i => !command.Images.Contains(i.Url))
                    .ToList() ?? [];
                if (toRemove.Count > 0)
                {
                    foreach (var image in toRemove)
                    {
                        _resourceService.DeleteResource(image);
                        product.RemoveImage(image);
                    }
                }
            }

            if(command.NewImages is not null)
            {
                foreach (var image in command.NewImages)
                {
                    product.AddImage(await _resourceService.SaveResource(image, FileType.Image));
                }
            }

            List<ProductCategory>? Categories = [];
            foreach (var categoryId in command.CategoryIds ?? [])
            {
                var category = await _categoryRepository.GetByIdAsync(categoryId);
                if (category is null)
                    continue;
                Categories.Add(category);
            }
            product.SyncCategories(Categories);

            _productRepository.Update(product);
            await _uow.SaveChangesAsync(ct);
            
            return new ApiResponse<UpdateProductResponse>
            {
                Success = true,
                Message = "Update Product Successfully",
                Code = 200,
                Data = new UpdateProductResponse{ProductId = product.Id}
            };
        }
    }
}