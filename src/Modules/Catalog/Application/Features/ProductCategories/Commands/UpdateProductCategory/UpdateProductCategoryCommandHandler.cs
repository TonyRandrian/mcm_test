using Mcm.Catalog.Application.Features.Products.Commands.UpdateProduct;
using Mcm.Catalog.Application.Interfaces;
using Mcm.Catalog.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Enums;
using MediatR;

namespace Mcm.Catalog.Application.Features.ProductCategories.Commands.UpdateProductCategory
{
    public class UpdateProductCategoryCommandHandler(IProductCategoryRepository categoryRepository, ICatalogUow uow)
        : IRequestHandler<UpdateProductCategoryCommand, ApiResponse<UpdateProductCategoryResponse>>
    {
        private readonly IProductCategoryRepository _categoryRepository = categoryRepository;
        private readonly ICatalogUow _uow = uow;

        public async Task<ApiResponse<UpdateProductCategoryResponse>> Handle(UpdateProductCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(command.Id)
                    ?? throw NotFoundException.NotFoundById(nameof(ProductCategory), command.Id);

            category.Update(command.Name);
    
            _categoryRepository.Update(category);
            await _uow.SaveChangesAsync(cancellationToken);
            
            return new ApiResponse<UpdateProductCategoryResponse>
            {
                Success = true,
                Message = "Update Product Category Successfully",
                Code = 200,
                Data = new UpdateProductCategoryResponse{ProductCategoryId = category.Id}
            };
        }
    }
}