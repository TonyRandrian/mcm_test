using Mcm.Catalog.Application.Features.Services.Commands.UpdateService;
using Mcm.Catalog.Application.Interfaces;
using Mcm.Catalog.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Enums;
using MediatR;

namespace Mcm.Catalog.Application.Features.ServiceCategories.Commands.UpdateServiceCategory
{
    public class UpdateServiceCategoryCommandHandler(IServiceCategoryRepository categoryRepository, ICatalogUow uow)
        : IRequestHandler<UpdateServiceCategoryCommand, ApiResponse<UpdateServiceCategoryResponse>>
    {
        private readonly IServiceCategoryRepository _categoryRepository = categoryRepository;
        private readonly ICatalogUow _uow = uow;
        
        public async Task<ApiResponse<UpdateServiceCategoryResponse>> Handle(UpdateServiceCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(command.Id)
                    ?? throw NotFoundException.NotFoundById(nameof(ServiceCategory), command.Id);

            category.Update(command.Name);
    
            _categoryRepository.Update(category);
            await _uow.SaveChangesAsync(cancellationToken);
            
            return new ApiResponse<UpdateServiceCategoryResponse>
            {
                Success = true,
                Message = "Update Service Category Successfully",
                Code = 200,
                Data = new UpdateServiceCategoryResponse{ServiceCategoryId = category.Id}
            };
        }
    }
}