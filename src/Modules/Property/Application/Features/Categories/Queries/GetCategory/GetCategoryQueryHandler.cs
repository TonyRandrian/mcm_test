using Mcm.Property.Application.Interfaces;
using Mcm.Property.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using MediatR;

namespace Mcm.Property.Application.Features.Categories.Queries.GetCategory
{
    public class GetCategoryQueryHandler(ICategoryRepository categoryRepository)
        : IRequestHandler<GetCategoryQuery, ApiResponse<GetCategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;

        public async Task<ApiResponse<GetCategoryResponse>> Handle(GetCategoryQuery query, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(query.Request.Id)
                ?? throw NotFoundException.NotFoundById(nameof(Category), query.Request.Id);
            
            return new ApiResponse<GetCategoryResponse>
            {
                Success = true,
                Message = "Category retrieved successfully",
                Code = 200,
                Data = new GetCategoryResponse
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    Properties = [.. category.Properties.Select(prop => new CategoryPropertyResponse
                    {
                        Id = prop.Id,
                        Name = prop.Name,
                        Type = prop.Type.ToString(),
                        IsRequired = prop.IsRequired,
                        IsMultiple = prop.IsMultiple,
                        IsSensitive = prop.IsSensitive
                    })]
                }
            };
        }
    }
}