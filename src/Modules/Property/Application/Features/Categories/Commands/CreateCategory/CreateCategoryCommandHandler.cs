using Mcm.Property.Application.Interfaces;
using Mcm.Property.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using MediatR;

namespace Mcm.Property.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IPropertyUow uow) : IRequestHandler<CreateCategoryCommand, ApiResponse<CreateCategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IPropertyUow _uow = uow;

        public async Task<ApiResponse<CreateCategoryResponse>> Handle(CreateCategoryCommand command, CancellationToken ct)
        {
            var cat = Category.Create(
                command.Name,
                command.Description
            );

            var exist = await _categoryRepository.Validate(c => c.Name.Value.Equals(cat.Name.Value));
            if (exist is not null)
            {
                if (exist.IsDeleted)
                {
                    exist.Restore();
                    _categoryRepository.Update(exist);
                }
            }
            else
            {
                foreach (var entity in command.EntityTypes)
                    cat.AddEntityType(entity);
                await _categoryRepository.AddAsync(cat);
            } 
                
            await _uow.SaveChangesAsync(ct);
            
            return new ApiResponse<CreateCategoryResponse>
            {
                Success = true,
                Message = "Category created successfully",
                Code = 200,
                Data = new CreateCategoryResponse{CategoryId = cat.Id}
            };
        }
    }
}