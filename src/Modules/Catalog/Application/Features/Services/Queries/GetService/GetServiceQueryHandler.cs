using Mcm.Catalog.Application.Interfaces;
using Mcm.Catalog.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using MediatR;

namespace Mcm.Catalog.Application.Features.Services.Queries.GetService
{
    public class GetServiceQueryHandler(IServiceRepository ServiceRepository)
        : IRequestHandler<GetServiceQuery, ApiResponse<GetServiceResponse>>
    {
        private readonly IServiceRepository _ServiceRepository = ServiceRepository;

        public async Task<ApiResponse<GetServiceResponse>> Handle(GetServiceQuery query, CancellationToken cancellationToken)
        {
            var service = await _ServiceRepository.GetByIdAsync(query.Request.Id)
                ?? throw NotFoundException.NotFoundById(nameof(Service), query.Request.Id);

            return new ApiResponse<GetServiceResponse>
            {
                Success = true,
                Message = "Service retrieved successfully",
                Code = 200,
                Data = new GetServiceResponse
                {
                    Id = service.Id,
                    Name = service.Name,
                    Description = service.Description,
                    MinPrice = service.MinPrice,
                    MaxPrice = service.MaxPrice,
                    Unit = service.Unit,
                    CoverPicture = new ResourceResponse(
                        service.CoverPicture.FileType.ToString(),
                        service.CoverPicture.Url,
                        service.CoverPicture.AlternativeText,
                        service.CoverPicture.StorageType.ToString()
                    ),
                    Images = service.Images.Select(i => new ResourceResponse(
                        i.FileType.ToString(),
                        i.Url,
                        i.AlternativeText,
                        i.StorageType.ToString()
                    )).ToList(),
                    Category = service.Category?.Name,
                    Currency = service.Currency.Name,
                }            
            };
        }
    }
}