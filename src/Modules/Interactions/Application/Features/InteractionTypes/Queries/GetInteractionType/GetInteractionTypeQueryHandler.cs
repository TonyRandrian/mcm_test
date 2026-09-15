using System.Text.Json;
using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using MediatR;

namespace Mcm.Interactions.Application.Features.InteractionTypes.Queries.GetInteractionType
{
    public class GetInteractionTypeQueryHandler(
        IInteractionTypeRepository interactionTypeRepository)
        : IRequestHandler<GetInteractionTypeQuery, ApiResponse<GetInteractionTypeResponse>>
    {
        private readonly IInteractionTypeRepository _interactionTypeRepository = interactionTypeRepository;

        public async Task<ApiResponse<GetInteractionTypeResponse>> Handle(GetInteractionTypeQuery query, CancellationToken cancellationToken)
        {
            var type = await _interactionTypeRepository.GetByIdAsync(query.Request.Id)
                    ?? throw NotFoundException.NotFoundById(nameof(InteractionType), query.Request.Id);

            var subtypes = await _interactionTypeRepository.GetAllAsync(
                predicate: it => it.ParentId == type.Id, 
                ct: cancellationToken);

            return new ApiResponse<GetInteractionTypeResponse>
            {
                Success = true,
                Message = "Get InteractionType successfully",
                Code = 200,
                Data = new GetInteractionTypeResponse
                {
                    Id = type.Id,
                    Title = type.Title,
                    LabelColor = type.LabelColor,
                    Description = type.Description,
                    SubTypes = subtypes.Select(st =>
                        new GetInteractionTypeResponse
                        {
                            Id = st.Id,
                            Title = st.Title,
                            LabelColor = st.LabelColor,
                            Description = st.Description,
                            SubTypes = null
                        }).ToList()
                }
            };
        }
    }
}