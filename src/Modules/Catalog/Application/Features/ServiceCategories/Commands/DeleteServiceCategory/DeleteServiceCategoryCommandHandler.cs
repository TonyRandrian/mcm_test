using Mcm.Catalog.Application.Features.Services.Commands.DeleteService;
using Mcm.Catalog.Application.Interfaces;
using Mcm.Catalog.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using MediatR;

namespace Mcm.Catalog.Application.Features.ServiceCategories.Commands.DeleteServiceCategory
{
    public class DeleteServiceCategoryCommandHandler(IServiceCategoryRepository ServiceCategoryRepository, ICatalogUow uow)
    : IRequestHandler<DeleteServiceCategoryCommand, ApiResponse<DeleteServiceCategoryResponse>>
    {
        private readonly IServiceCategoryRepository _serviceCategoryRepository = ServiceCategoryRepository;
        private readonly ICatalogUow _uow = uow;

        public async Task<ApiResponse<DeleteServiceCategoryResponse>> Handle(DeleteServiceCategoryCommand command, CancellationToken cancellationToken)
        {
            var serviceCategory = await _serviceCategoryRepository.GetByIdAsync(command.Id)
                    ?? throw NotFoundException.NotFoundById(nameof(ServiceCategory), command.Id);

            if (command.Force)
                _serviceCategoryRepository.HardDelete(serviceCategory);
            else
            {
                serviceCategory.Delete();
                _serviceCategoryRepository.Update(serviceCategory);
            }
        
            await _uow.SaveChangesAsync(cancellationToken);    
            return new ApiResponse<DeleteServiceCategoryResponse>
            {
                Success = true,
                Message = "Delete Service Category Successfully",
                Code = 200,
                Data = new DeleteServiceCategoryResponse{Unit = Unit.Value}
            };
        }
    }
}