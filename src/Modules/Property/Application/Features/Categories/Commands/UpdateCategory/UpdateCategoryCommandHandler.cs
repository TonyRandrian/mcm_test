using Mcm.Property.Application.Interfaces;
using Mcm.Property.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using MediatR;

namespace Mcm.Property.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IPropertyUow uow) : IRequestHandler<UpdateCategoryCommand, ApiResponse<UpdateCategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IPropertyUow _uow = uow;
        public async Task<ApiResponse<UpdateCategoryResponse>> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            var cat = await _categoryRepository.GetByIdAsync(command.Id)
                    ?? throw NotFoundException.NotFoundById(nameof(Category), command.Id);
            
            if (cat.IsSystem)
                throw new UnauthorizedException("not authorized to update this category");

            cat.Update(command.Name, command.Description);
            cat.SyncListEntityType(command.EntityTypes);
    
            // _categoryRepository.Update(cat);
            await _uow.SaveChangesAsync(cancellationToken);
            
            return new ApiResponse<UpdateCategoryResponse>
            {
                Success = true,
                Message = "Update Category Successfully",
                Code = 200,
                Data = new UpdateCategoryResponse{Id = cat.Id}
            };
        }
    }
}