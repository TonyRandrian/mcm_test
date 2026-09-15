using Mcm.Property.Application.Interfaces;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using MediatR;

namespace Mcm.Property.Application.Features.Properties.Queries.GetProperty
{
    public class GetPropertyQueryHandler(IPropertyRepository propertyRepository) : IRequestHandler<GetPropertyQuery, ApiResponse<GetPropertyResponse>>
    {
        private readonly IPropertyRepository _propertyRepository = propertyRepository;
        
        public async Task<ApiResponse<GetPropertyResponse>> Handle(GetPropertyQuery query, CancellationToken cancellationToken)
        {
            var res = await _propertyRepository.GetByIdAsync(query.Request.Id)
                    ?? throw NotFoundException.NotFoundById(nameof(Domain.Entities.Property), query.Request.Id);
            
            return new ApiResponse<GetPropertyResponse>
            {
                Success = true,
                Message = "Get one Property",
                Code = 200,
                Data = new GetPropertyResponse {
                    Id = res.Id,
                    Name = res.Name,
                    Type = res.Type.ToString(),
                    Description = res.Description,
                    IsRequired = res.IsRequired, 
                    IsMultiple = res.IsMultiple,
                    IsSensitive = res.IsSensitive 
                }
            };
        }
    }
}