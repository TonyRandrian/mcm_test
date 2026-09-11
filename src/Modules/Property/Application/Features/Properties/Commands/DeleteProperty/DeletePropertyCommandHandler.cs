using Mcm.Property.Application.Interfaces;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using MediatR;

namespace Mcm.Property.Application.Features.Properties.Commands.DeleteProperty
{
    public class DeletePropertyCommandHandler(IPropertyRepository propertyRepository, IPropertyUow uow) : IRequestHandler<DeletePropertyCommand, ApiResponse<DeletePropertyResponse>>
    {
        private readonly IPropertyRepository _propertyRepository = propertyRepository;
        private readonly IPropertyUow _uow = uow;

        public async Task<ApiResponse<DeletePropertyResponse>> Handle(DeletePropertyCommand command, CancellationToken ct)
        {
            var prop = await _propertyRepository.GetByIdAsync(command.Id)
                    ?? throw NotFoundException.NotFoundById(nameof(Domain.Entities.Property), command.Id);
            
            if (prop.IsSystem)
                throw new UnauthorizedException("deleting this property not authorized");

            if (command.Force)
                _propertyRepository.HardDelete(prop);
            else
            {
                prop.Delete();
                _propertyRepository.Update(prop);
            }
        
            await _uow.SaveChangesAsync(ct);

            return new ApiResponse<DeletePropertyResponse>
            {
                Success = true,
                Message = "DeleteProperty",
                Code = 200,
                Data = new DeletePropertyResponse{Unit = Unit.Value}
            };
        }
    }
}