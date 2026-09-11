using Mcm.Catalog.Application.Interfaces;
using Mcm.Catalog.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Enums;
using MediatR;

namespace Mcm.Catalog.Application.Features.ServiceCategories.Commands.CreateServiceCategory
{
    public class CreateServiceCategoryCommandHandler(IServiceCategoryRepository serviceCategoryRepository, ICatalogUow uow)
    : IRequestHandler<CreateServiceCategoryCommand, ApiResponse<CreateServiceCategoryResponse>>
    {
        private readonly IServiceCategoryRepository _ServiceCategoryRepository = serviceCategoryRepository;
        private readonly ICatalogUow _uow = uow;

        public async Task<ApiResponse<CreateServiceCategoryResponse>> Handle(CreateServiceCategoryCommand command, CancellationToken cancellationToken)
        {
            var serviceCategory = ServiceCategory.Create(
                command.ParentCategoryId,
                command.Name
            );

            await _ServiceCategoryRepository.AddAsync(serviceCategory);   
            await _uow.SaveChangesAsync(cancellationToken);
            
            return new ApiResponse<CreateServiceCategoryResponse>
            {
                Success = true,
                Message = "Service created successfully",
                Code = 200,
                Data = new CreateServiceCategoryResponse{ServiceCategoryId = serviceCategory.Id}
            };
        }
    }
}