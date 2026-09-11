using Mcm.Catalog.Application.Features.Products.Commands.DeleteProduct;
using Mcm.Catalog.Application.Interfaces;
using Mcm.Catalog.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using MediatR;

namespace Mcm.Catalog.Application.Features.ProductCategories.Commands.DeleteProductCategory
{
    public class DeleteProductCategoryCommandHandler(IProductCategoryRepository productCategoryRepository, ICatalogUow uow)
    : IRequestHandler<DeleteProductCategoryCommand, ApiResponse<DeleteProductCategoryResponse>>
    {
        private readonly IProductCategoryRepository _productCategoryRepository = productCategoryRepository;
        private readonly ICatalogUow _uow = uow;

        public async Task<ApiResponse<DeleteProductCategoryResponse>> Handle(DeleteProductCategoryCommand command, CancellationToken cancellationToken)
        {
            var productCategory = await _productCategoryRepository.GetByIdAsync(command.Id)
                    ?? throw NotFoundException.NotFoundById(nameof(ProductCategory), command.Id);

            if (command.Force)
                _productCategoryRepository.HardDelete(productCategory);
            else
            {
                productCategory.Delete();
                _productCategoryRepository.Update(productCategory);
            }
        
            await _uow.SaveChangesAsync(cancellationToken);    
            return new ApiResponse<DeleteProductCategoryResponse>
            {
                Success = true,
                Message = "Delete Product Category Successfully",
                Code = 200,
                Data = new DeleteProductCategoryResponse{Unit = Unit.Value}
            };
        }
    }
}