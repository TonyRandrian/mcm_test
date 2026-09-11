using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using MediatR;

namespace Mcm.Interactions.Application.Features.TypeFields.Queries.GetTypeField
{
    public class GetTypeFieldQueryHandler(
        ITypeFieldRepository fieldRepository)
        : IRequestHandler<GetTypeFieldQuery, ApiResponse<GetTypeFieldResponse>>
    {
        private readonly ITypeFieldRepository _fieldRepository = fieldRepository;

        public async Task<ApiResponse<GetTypeFieldResponse>> Handle(GetTypeFieldQuery query, CancellationToken cancellationToken)
        {
            var field = await _fieldRepository.GetByIdAsync(query.Request.Id)
                    ?? throw NotFoundException.NotFoundById(nameof(TypeField), query.Request.Id);

            return new ApiResponse<GetTypeFieldResponse>
            {
                Success = true,
                Message = "Get TypeField successfully",
                Code = 200,
                Data = new GetTypeFieldResponse
                {
                    Id = field.Id,
                    Name = field.Name,
                    Type = field.Type.ToString()
                }
            };
        }
    }
}