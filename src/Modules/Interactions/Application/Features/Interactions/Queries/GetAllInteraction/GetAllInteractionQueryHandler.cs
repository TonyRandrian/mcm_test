using System.Text.Json;
using Mcm.Interactions.Application.Features.InteractionTypes.Queries.GetAllInteractionType;
using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Domain.Extensions;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using Mcm.Shared.Application.Modules.DTOs;
using MediatR;

namespace Mcm.Interactions.Application.Features.Interactions.Queries.GetAllInteraction
{
    public class GetAllInteractionQueryHandler(
        IInteractionRepository interactionRepository)
        : IRequestHandler<GetAllInteractionQuery, ApiResponse<List<GetAllInteractionResponse>>>
    {
        private readonly IInteractionRepository _interactionRepository = interactionRepository;
        public async Task<ApiResponse<List<GetAllInteractionResponse>>> Handle(
            GetAllInteractionQuery query, CancellationToken cancellationToken)
        {
            var interactions = await _interactionRepository.GetAllAsync(
                predicate: c =>
                    (string.IsNullOrEmpty(query.Request.Search)
                        || c.Title.Value.ToLower().Contains(query.Request.Search.ToLower())) &&
                    (query.Request.TeamMemberId == null || c.InteractionMembers.Any(i => i.TeamMemberId == query.Request.TeamMemberId)) &&
                    (query.Request.ContactId == null || c.InteractionContacts.Any(i => i.ContactId == query.Request.ContactId)) &&
                    (query.Request.StartDateFrom == null || c.Date.StartDate >= query.Request.StartDateFrom) &&
                    (query.Request.StartDateTo == null || c.Date.StartDate <= query.Request.StartDateTo) &&
                    (query.Request.EndDateFrom == null || c.Date.EndDate >= query.Request.EndDateFrom) &&
                    (query.Request.EndDateTo == null || c.Date.EndDate <= query.Request.EndDateTo),
                orderBy: q => q.OrderByDescending(c => c.CreatedAt),
                pageQuery: new PageQuery(query.Request.Page, query.Request.Limit),
                ct: cancellationToken);

            return new ApiResponse<List<GetAllInteractionResponse>>
            {
                Success = true,
                Message = "Interactions retrieved successfully",
                Code = 200,
                Data = interactions.Select(i =>
                    new GetAllInteractionResponse{
                        Id = i.Id,
                        Title = i.Title,
                        Type = new GetAll_Type(
                            i.TypeId,
                            i.Type.Title,
                            i.Type.LabelColor),
                        Date = new GetAll_Date(
                            i.Date.StartDate.GetDate(),
                            i.Date.StartDate.GetHour(),
                            i.Date.EndDate.GetDate(),
                            i.Date.EndDate.GetHour()),
                        ReportId = i.ReportId,
                        AttachmentCount = i.AttachmentCount()
                    }).ToList(),
                Meta = new Meta
                {
                    Page  = query.Request.Page,
                    Limit = query.Request.Limit,
                    Total = await _interactionRepository.CountAsync()
                }
            };
        }
    }
}