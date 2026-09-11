using Mcm.Catalog.Application.Features.Services.Commands.UpdateService;
using Mcm.Catalog.Application.Interfaces;
using Mcm.Catalog.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Enums;
using MediatR;

namespace Mcm.Catalog.Application.Features.Services.Commands.UpdateService
{
    public class UpdateServiceCommandHandler(IServiceRepository serviceRepository, IServiceCategoryRepository categoryRepository, IResourceService resourceService, ICatalogUow uow) : IRequestHandler<UpdateServiceCommand, ApiResponse<UpdateServiceResponse>>
    {
        private readonly IServiceRepository _serviceRepository = serviceRepository;
        private readonly IServiceCategoryRepository _categoryRepository = categoryRepository;
        private readonly IResourceService _resourceService = resourceService;
        private readonly ICatalogUow _uow = uow;
        public async Task<ApiResponse<UpdateServiceResponse>> Handle(UpdateServiceCommand command, CancellationToken ct)
        {
            var service = await _serviceRepository.GetByIdAsync(command.Id)
                    ?? throw NotFoundException.NotFoundById(nameof(Service), command.Id);
            service.Update(
                command.Name,
                command.Description,
                command.MinPrice, 
                command.MaxPrice,
                command.Unit,
                command.CurrencyId,
                command.CategoryId
            );
            
            if(command.CoverPicture is not null)
            {
                if (service.CoverPicture is not null)
                    _resourceService.DeleteResource(service.CoverPicture);
                var coverPicture = await _resourceService.SaveResource(command.CoverPicture, FileType.Image);
                service.UpdateCoverPicture(coverPicture);
            }
        
            if (command.Images is not null)
            {
                var toRemove = service.Images?
                    .Where(i => !command.Images.Contains(i.Url))
                    .ToList() ?? [];
                if (toRemove.Count > 0)
                {
                    foreach (var image in toRemove)
                    {
                        _resourceService.DeleteResource(image);
                        service.RemoveImage(image);
                    }
                }
            }

            if(command.NewImages is not null)
            {
                foreach (var image in command.NewImages)
                {
                    service.AddImage(await _resourceService.SaveResource(image, FileType.Image));
                }
            }

            _serviceRepository.Update(service);
            await _uow.SaveChangesAsync(ct);
            
            return new ApiResponse<UpdateServiceResponse>
            {
                Success = true,
                Message = "Update Service Successfully",
                Code = 200,
                Data = new UpdateServiceResponse{ServiceId = service.Id}
            };
        }
    }
}