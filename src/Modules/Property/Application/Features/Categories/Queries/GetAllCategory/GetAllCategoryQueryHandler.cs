using Mcm.Property.Application.Features.Properties.Queries;
using Mcm.Property.Application.Interfaces;
using Mcm.Property.Domain.Enums;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.Extensions;
using MediatR;

namespace Mcm.Property.Application.Features.Categories.Queries.GetAllCategory
{
    public class GetAllCategoryQueryHandler(ICategoryRepository CategoryRepository)
        : IRequestHandler<GetAllCategoryQuery, ApiResponse<GetAllCategoryResponse>>
    {
        private readonly ICategoryRepository _CategoryRepository = CategoryRepository;
        
        public async Task<ApiResponse<GetAllCategoryResponse>> Handle(GetAllCategoryQuery query, CancellationToken cancellationToken)
        {
            var categories = await _CategoryRepository.GetAllAsync(
                predicate: (c => 
                    (string.IsNullOrEmpty(query.Request.Filter)
                        ||  c.Name.Value.ToLower().Contains(query.Request.Filter.ToLower()))
                    ||  c.IsSystem==query.Request.SystemOnly),
                orderBy: (c => 
                    c.OrderDescending()),
                pageQuery: new PageQuery(query.Request.Page, query.Request.Limit)
                
            );
            
            var data = categories.Select(res => new CategoryResponse
            {
                Id = res.Id,
                Name = res.Name,
                Properties = [.. res.Properties.Select(prop => new PropertyResponse
                {
                    Id = prop.Id,
                    Name = prop.Name,
                    Type = prop.Type.ToString(),
                    IsMultiple = prop.IsMultiple
                })]
            }).ToList();

            return new ApiResponse<GetAllCategoryResponse>
            {
                Success = true,
                Message = "Categories retrieved successfully",
                Code = 200,
                Data = new GetAllCategoryResponse
                {
                    Categories = data
                },
                Meta = new Meta
                {
                    Page = query.Request.Page,
                    Limit = query.Request.Limit,
                    Total = await _CategoryRepository.CountAsync()
                }
            };
        }
    }
}