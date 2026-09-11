using Mcm.Property.Application.Interfaces;
using Mcm.Property.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using MediatR;

namespace Mcm.Property.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IPropertyUow uow) : IRequestHandler<DeleteCategoryCommand, ApiResponse<DeleteCategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IPropertyUow _uow = uow;

        public async Task<ApiResponse<DeleteCategoryResponse>> Handle(DeleteCategoryCommand command, CancellationToken ct)
        {
            var category = await _categoryRepository.GetByIdAsync(command.Id)
                    ?? throw NotFoundException.NotFoundById(nameof(Category), command.Id);
            
            if (category.IsSystem)
                throw new UnauthorizedException("Cannot delete System category");
            if (command.Force)
                _categoryRepository.HardDelete(category);
            else
            {
                category.Delete();
                _categoryRepository.Update(category);
            }
        
            await _uow.SaveChangesAsync(ct); 

            return new ApiResponse<DeleteCategoryResponse>
            {
                Success = true,
                Message = "Delete Category Successfully",
                Code = 200,
                Data = new DeleteCategoryResponse{Unit = Unit.Value}
            };
        }
    }
}