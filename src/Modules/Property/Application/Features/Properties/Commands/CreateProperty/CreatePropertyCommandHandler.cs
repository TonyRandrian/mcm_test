using Mcm.Property.Application.Interfaces;
using Mcm.Property.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using MediatR;

namespace Mcm.Property.Application.Features.Properties.Commands.CreateProperty
{
    public class CreatePropertyCommandHandler(ICategoryRepository categoryRepository, IPropertyRepository propertyRepository, IPropertyUow uow)
        : IRequestHandler<CreatePropertyCommand, ApiResponse<CreatePropertyResponse>>
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IPropertyRepository _propertyRepository = propertyRepository;
        private readonly IPropertyUow _uow = uow;
        
        public async Task<ApiResponse<CreatePropertyResponse>> Handle(CreatePropertyCommand command, CancellationToken ct)
        {
            var cat = await _categoryRepository.GetByIdAsync(command.CategoryId)
                    ?? throw NotFoundException.NotFoundById(nameof(Category), command.CategoryId);
            
            var property = Domain.Entities.Property.Create(
                command.CategoryId, 
                command.Name, 
                command.Description, 
                command.Type,
                command.IsRequired,
                command.IsMultiple);
                
            var exist = await _propertyRepository.Validate( 
                p => p.CategoryId == property.CategoryId && p.Name.Value.Equals(property.Name.Value));
            if (exist is not null)
            {
                if (exist.IsDeleted)
                {
                    property.Restore();
                    _propertyRepository.Update(property);
                }
            }
            else
                await _propertyRepository.AddAsync(property);
            
            await _uow.SaveChangesAsync(ct);
            
            return new ApiResponse<CreatePropertyResponse>
            {
                Success = true,
                Message = "Create Property Succesfully",
                Code = 200,
                Data = new CreatePropertyResponse{Id = cat.Id}
            };
        }
    }
}