using Mcm.Property.Application.Interfaces;
using Mcm.Property.Domain.Entities;
using Mcm.Property.Domain.Enums;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Exceptions;
using MediatR;

namespace Mcm.Property.Application.Features.Categories.Commands.SetCategoryVisibility
{
    public class SetCategoryVisibilityCommandHandler(
        ICategoryRepository categoryRepository,
        ICategorySettingRepository settingRepository,
        IPropertyUow uow)
        : IRequestHandler<SetCategoryVisibilityCommand, ApiResponse<SetCategoryVisibilityResponse>>
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly ICategorySettingRepository _settingRepository = settingRepository;
        private readonly IPropertyUow _uow = uow;

        public async Task<ApiResponse<SetCategoryVisibilityResponse>> Handle(SetCategoryVisibilityCommand command, CancellationToken cancellationToken)
        {
            var cat = await _categoryRepository.GetByIdAsync(command.CategoryId)
                    ?? throw NotFoundException.NotFoundById(nameof(Category), command.CategoryId);

            if (!Enum.TryParse<EntityType>(command.EntityType, ignoreCase: true, out var entityType))
                throw new DomainException("Unknown entity type.");

            if (!cat.Entities.Any(e => e.EntityType == entityType))
                throw new DomainException("This category is not linked to the specified entity type.");

            var setting = await _settingRepository.Validate(s =>
                s.CategoryId == command.CategoryId && s.EntityType == entityType);
            if (setting is null)
            {
                setting = CategorySetting.Create(
                    command.CategoryId, entityType, command.IsVisibleInProfile);
                await _settingRepository.AddAsync(setting);
            }
            else
            {
                setting.Update(command.IsVisibleInProfile);
                _settingRepository.Update(setting);
            }

            await _uow.SaveChangesAsync(cancellationToken);

            return new ApiResponse<SetCategoryVisibilityResponse>
            {
                Success = true,
                Message = "Category visibility updated successfully",
                Code = 200,
                Data = new SetCategoryVisibilityResponse { CategoryId = cat.Id }
            };
        }
    }
}