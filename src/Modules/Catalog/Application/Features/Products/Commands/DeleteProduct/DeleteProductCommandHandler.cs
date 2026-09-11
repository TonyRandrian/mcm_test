using Mcm.Catalog.Application.Interfaces;
using Mcm.Catalog.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using MediatR;

namespace Mcm.Catalog.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler(IProductRepository productRepository, ICatalogUow uow)
    : IRequestHandler<DeleteProductCommand, ApiResponse<DeleteProductResponse>>
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly ICatalogUow _uow = uow;

        public async Task<ApiResponse<DeleteProductResponse>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(command.Id)
                    ?? throw NotFoundException.NotFoundById(nameof(Product), command.Id);

            if (command.Force)
                _productRepository.HardDelete(product);
            else
            {
                product.Delete();
                _productRepository.Update(product);
            }
        
            await _uow.SaveChangesAsync(cancellationToken);    
            return new ApiResponse<DeleteProductResponse>
            {
                Success = true,
                Message = "Delete Product Successfully",
                Code = 200,
                Data = new DeleteProductResponse{Unit = Unit.Value}
            };
        }
    }
}