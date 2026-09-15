using Mcm.Catalog.Application.Interfaces;
using Mcm.Catalog.Domain.Entities;
using Mcm.Property.Domain.Enums;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Interfaces;
using MediatR;

namespace Mcm.Catalog.Application.Features.Services.Queries.GetAllService
{
    public class GetAllServiceQueryHandler(
        IServiceRepository ServiceRepository,
        ICurrentUserService currentUserService)
        : IRequestHandler<GetAllServiceQuery, ApiResponse<GetAllServiceResponse>>
    {
        private readonly IServiceRepository _ServiceRepository = ServiceRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        
        public async Task<ApiResponse<GetAllServiceResponse>> Handle(GetAllServiceQuery query, CancellationToken cancellationToken)
        {
            var services = await _ServiceRepository.GetAllAsync(
                includes: [
                    s => s.Currency,
                    s => s.Category
                ],
                predicate: p => 
                    (string.IsNullOrEmpty(query.Request.SearchName) || 
                    p.Name.Contains(query.Request.SearchName)) && 
                    p.CompanyId == _currentUserService.CompanyId, 
                orderBy: p => p.OrderBy(s => s.Name), 
                pageQuery: new PageQuery(query.Request.Page, query.Request.Limit), 
                ct: cancellationToken);
            var data = services.Select(res => new ServiceResponse
            {
                Id = res.Id,
                Name = res.Name,
                Description = res.Description,
                MaxPrice = res.MaxPrice,
                MinPrice = res.MinPrice,
                Unit = res.Unit,
                CoverPicture = new ResourceResponse
                (
                    res.CoverPicture.FileType.ToString(),
                    res.CoverPicture.Url,
                    res.CoverPicture.AlternativeText,
                    res.CoverPicture.StorageType.ToString()
                ),
                Images = res.Images.Select(i => new ResourceResponse
                (
                    i.FileType.ToString(),
                    i.Url,
                    i.AlternativeText,
                    i.StorageType.ToString()
                )).ToList(),
                Category = res.Category?.Name ?? string.Empty,
                Currency = res.Currency.Symbol
            }).ToList();

            return new ApiResponse<GetAllServiceResponse>
            {
                Success = true,
                Message = "Services retrieved successfully",
                Code = 200,
                Data = new GetAllServiceResponse(data),
                Meta = new Meta
                {
                    Page = query.Request.Page,
                    Limit = query.Request.Limit,
                    Total = await _ServiceRepository.CountAsync()
                }
            };
        }
    }
}