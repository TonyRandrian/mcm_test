using System.Text.Json;
using Mcm.Interactions.Application.Interfaces;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using Mcm.Shared.Application.Modules.DTOs;
using MediatR;

namespace Mcm.Interactions.Application.Features.InteractionTypes.Queries.GetAllInteractionType
{
    public class GetAllInteractionTypeQueryHandler(
        IInteractionTypeRepository typeRepository)
        : IRequestHandler<GetAllInteractionTypeQuery, ApiResponse<List<GetAllInteractionTypeResponse>>>
    {
        private readonly IInteractionTypeRepository _interactionTypeRepository = typeRepository;
        
        public async Task<ApiResponse<List<GetAllInteractionTypeResponse>>> Handle(
            GetAllInteractionTypeQuery query, CancellationToken cancellationToken)
        {
            var types = await _interactionTypeRepository.GetAllAsync(
                predicate: c =>
                    c.ParentId == null &&
                    (string.IsNullOrEmpty(query.Request.Search)
                        || c.Title.Value.ToLower().Contains(query.Request.Search.ToLower())),
                orderBy: q => q.OrderByDescending(c => c.CreatedAt),
                pageQuery: new PageQuery(query.Request.Page, query.Request.Limit),
                ct: cancellationToken);

            return new ApiResponse<List<GetAllInteractionTypeResponse>>
            {
                Success = true,
                Message = "InteractionTypes retrieved successfully",
                Code = 200,
                Data = types.Select(t =>
                    new GetAllInteractionTypeResponse
                    {
                        Id = t.Id,
                        Title = t.Title,
                        LabelColor = t.LabelColor
                    }).ToList(),
                Meta = new Meta
                {
                    Page  = query.Request.Page,
                    Limit = query.Request.Limit,
                    Total = await _interactionTypeRepository.CountAsync(
                        predicate: c =>
                            c.ParentId == null &&
                            (string.IsNullOrEmpty(query.Request.Search)
                                || c.Title.Value.ToLower().Contains(query.Request.Search.ToLower())))
                }
            };
        }
    }
}