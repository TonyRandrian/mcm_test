using Mcm.Catalog.Application.Interfaces;
using Mcm.Catalog.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Enums;
using MediatR;

namespace Mcm.Catalog.Application.Features.Services.Commands.CreateService
{
    public class CreateServiceCommandHandler(IServiceRepository serviceRepository, ICurrencyRepository currencyRepository, ICurrentUserService currentUserService, IServiceCategoryRepository ServiceCategoryRepository, IResourceService resourceService, ICatalogUow uow) : IRequestHandler<CreateServiceCommand, ApiResponse<CreateServiceResponse>>
    {
        private readonly IServiceRepository _serviceRepository = serviceRepository;
        private readonly IResourceService _resourceService = resourceService;
        private readonly IServiceCategoryRepository _ServiceCategoryRepository = ServiceCategoryRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly ICurrencyRepository _currencyRepository = currencyRepository;
        private readonly ICatalogUow _uow = uow;

        public async Task<ApiResponse<CreateServiceResponse>> Handle(CreateServiceCommand command, CancellationToken cancellationToken)
        {
            var currency = await _currencyRepository.GetByIdAsync(command.CurrencyId)
                ?? throw NotFoundException.NotFoundById(nameof(Currency), command.CurrencyId);

            var service = Service.Create(
                _currentUserService.CompanyId,
                command.Name,
                command.Description,
                command.MinPrice,
                command.MaxPrice,
                command.Unit,
                currency.Id,
                command.CategoryId
            );
    
            service.UpdateCoverPicture(await _resourceService.SaveResource(command.CoverPicture, FileType.Image));
            
            if (command.Images is not null)
                foreach (var image in command.Images)
                {
                    service.AddImage(await _resourceService.SaveResource(image, FileType.Image));
                }

            await _serviceRepository.AddAsync(service);    
            await _uow.SaveChangesAsync(cancellationToken);
            
            return new ApiResponse<CreateServiceResponse>
            {
                Success = true,
                Message = "Service created successfully",
                Code = 200,
                Data = new CreateServiceResponse{ServiceId = service.Id}
            };
        }
    }
}