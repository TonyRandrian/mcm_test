using Mcm.Catalog.Application.Interfaces;
using Mcm.Catalog.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using MediatR;

namespace Mcm.Catalog.Application.Features.Services.Commands.DeleteService
{
    public class DeleteServiceCommandHandler(IServiceRepository ServiceRepository, ICatalogUow uow)
    : IRequestHandler<DeleteServiceCommand, ApiResponse<DeleteServiceResponse>>
    {
        private readonly IServiceRepository _ServiceRepository = ServiceRepository;
        private readonly ICatalogUow _uow = uow;

        public async Task<ApiResponse<DeleteServiceResponse>> Handle(DeleteServiceCommand command, CancellationToken cancellationToken)
        {
            var service = await _ServiceRepository.GetByIdAsync(command.Id)
                    ?? throw NotFoundException.NotFoundById(nameof(Service), command.Id);

            if (command.Force)
                _ServiceRepository.HardDelete(service);
            else
            {
                service.Delete();
                _ServiceRepository.Update(service);
            }
        
            await _uow.SaveChangesAsync(cancellationToken);    
            return new ApiResponse<DeleteServiceResponse>
            {
                Success = true,
                Message = "Delete Service Successfully",
                Code = 200,
                Data = new DeleteServiceResponse{Unit = Unit.Value}
            };
        }
    }
}