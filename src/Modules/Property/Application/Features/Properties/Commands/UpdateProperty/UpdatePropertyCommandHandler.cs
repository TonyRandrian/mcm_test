using Mcm.Property.Application.Interfaces;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using MediatR;

namespace Mcm.Property.Application.Features.Properties.Commands.UpdateProperty
{
    public class UpdatePropertyCommandHandler(IPropertyRepository propertyRepository, IPropertyUow uow) : IRequestHandler<UpdatePropertyCommand, ApiResponse<UpdatePropertyResponse>>
    {
        private readonly IPropertyRepository _propertyRepository = propertyRepository;
        private readonly IPropertyUow _uow = uow;
        public async Task<ApiResponse<UpdatePropertyResponse>> Handle(UpdatePropertyCommand command, CancellationToken ct)
        {
            var prop = await _propertyRepository.GetByIdAsync(command.Id)
                    ?? throw NotFoundException.NotFoundById(nameof(Domain.Entities.Property), command.Id);
            
            if (prop.IsSystem)
                throw new UnauthorizedException("Updating system property not authorized");
            
            prop.Update(
                command.Name, 
                command.Description, 
                command.Type);
            
            _propertyRepository.Update(prop);
            
            await _uow.SaveChangesAsync(ct);
            
            return new ApiResponse<UpdatePropertyResponse>
            {
                Success = true,
                Message = "Update Property Successful",
                Code = 200,
                Data = new UpdatePropertyResponse{Id = prop.Id}
            };
        }
    }
}