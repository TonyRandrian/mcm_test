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
            EntityType entityType = EntityType.Other;
            if (query.Request.Filter is not null)
                if (!Enum.TryParse<EntityType>(query.Request.Filter, out entityType))
                    throw new ArgumentException("Unknown Filter CategoryEntity");
            
            var categories = await _CategoryRepository.GetAllAsync(
                includes: [
                    c => c.Properties,
                    c => c.Entities
                ],
                predicate: c => 
                    (string.IsNullOrEmpty(query.Request.Filter) ||
                        c.Entities.Any(e => e.EntityType == Enum.Parse<EntityType>(query.Request.Filter))) &&
                    (string.IsNullOrEmpty(query.Request.SearchByName) ||
                        c.Name.Value.ToLower().Contains(query.Request.SearchByName.ToLower())) &&
                    query.Request.SystemOnly
                        ? c.IsSystem
                        : c.IsSystem | !c.IsSystem,
                orderBy: c => c.OrderBy(c => c.Name.Value),
                pageQuery: new PageQuery(query.Request.Page, query.Request.Limit),
                ct: cancellationToken
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
                    IsMultiple = prop.IsMultiple,
                    IsRequired = prop.IsRequired,
                    IsSensitive = prop.IsSensitive
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
                    Total = await _CategoryRepository.CountAsync(
                        predicate: c => 
                            (string.IsNullOrEmpty(query.Request.Filter) ||
                                c.Entities.Any(e => e.EntityType == Enum.Parse<EntityType>(query.Request.Filter))) &&
                            (string.IsNullOrEmpty(query.Request.SearchByName) ||
                                c.Name.Value.ToLower().Contains(query.Request.SearchByName.ToLower())) &&
                            query.Request.SystemOnly
                                ? c.IsSystem
                                : c.IsSystem | !c.IsSystem)
                }
            };
        }
    }
}