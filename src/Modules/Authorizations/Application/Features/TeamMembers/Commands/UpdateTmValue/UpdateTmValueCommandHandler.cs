using Mcm.Authorizations.Application.Features.TeamMembers.Commands.UpdateTmValue;
using Mcm.Authorizations.Application.Interfaces;
using Mcm.Authorizations.Domain.Entities;
using Mcm.Company.Application.Interfaces;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using MediatR;

namespace Mcm.Authorizations.Application.Features.TeamMembers.Commands.UpdatetTmValue
{
    public class UpdatetTmValueCommandHandler(
        ICurrentUserService currentUserService,
        ITeamMemberRepository teamMemberRepository, 
        ICategoryModule categoryModule, 
        IAuthorizationUow uow)
        : IRequestHandler<UpdateTmValueCommand, ApiResponse<UpdateTmValueResponse>>
    {
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly ITeamMemberRepository _tmRepository = teamMemberRepository;
        private readonly ICategoryModule _categoryModule = categoryModule;
        private readonly IAuthorizationUow _uow = uow;
        public async Task<ApiResponse<UpdateTmValueResponse>> Handle(UpdateTmValueCommand command, CancellationToken cancellationToken)
        {
            var teamMember = await _tmRepository.GetByIdAsync(command.TeamMemberId)
                ?? throw NotFoundException.NotFoundById(nameof(TeamMember), command.TeamMemberId);
            
            foreach (var valueRequest in command.Values)
            {
                var category = await _categoryModule.GetByIdAsync(valueRequest.CategoryId)
                    ?? throw NotFoundException.NotFoundById("Category", valueRequest.CategoryId);

                var propertyMap = category.Properties.ToDictionary(p => p.Id);

                var validRequests = valueRequest.Informations
                    .Where(i => propertyMap.ContainsKey(i.PropertyId))
                    .ToList();
                var requestedPropertyIds = validRequests.Select(i => i.PropertyId).ToHashSet();

                foreach (var property in category.Properties)
                {
                    if (!requestedPropertyIds.Contains(property.Id))
                    {
                        teamMember.RemoveAllValuesByProperty(property.Id);
                    }
                }

                var grouped = validRequests.GroupBy(i => i.PropertyId);
                foreach (var group in grouped)
                {
                    var propertyId = group.Key;
                    var property = propertyMap[propertyId];

                    if (!property.IsMultiple)
                    {
                        teamMember.UpdateValue(group.First().Value, propertyId);
                    }
                    else
                    {
                        teamMember.RemoveAllValuesByProperty(propertyId);
                        foreach (var item in group)
                        {
                            teamMember.AddValue(item.Value, propertyId, isMultiple: true);
                        }
                    }
                }
            }

            await _uow.SaveChangesAsync(cancellationToken);

            return new ApiResponse<UpdateTmValueResponse>
            {
                Success = true,
                Message = "Company value updated successfully",
                Code = 200,
                Data = new UpdateTmValueResponse{TeamMemberId = teamMember.Id}
            };
        }
    }
}