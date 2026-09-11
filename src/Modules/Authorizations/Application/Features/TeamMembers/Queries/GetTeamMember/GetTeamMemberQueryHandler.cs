using Mcm.Authorizations.Application.Interfaces;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using MediatR;

namespace Mcm.Authorizations.Application.Features.TeamMembers.Queries.GetTeamMember
{
    public class GetTeamMemberQueryHandler(ITeamMemberRepository teamMemberRepository, ICategoryModule categoryModule)
        : IRequestHandler<GetTeamMemberQuery, ApiResponse<GetTeamMemberResponse>>
    {
        public ITeamMemberRepository _teamMemberRepository = teamMemberRepository;
        public ICategoryModule _categoryModule = categoryModule;

        public async Task<ApiResponse<GetTeamMemberResponse>> Handle(GetTeamMemberQuery query, CancellationToken cancellationToken)
        {
            var teamMember = await _teamMemberRepository.GetByIdAsync(query.Header.Id)
                    ?? throw NotFoundException.NotFoundById(nameof(Authorizations.Domain.Entities.TeamMember), query.Header.Id);

            var propertyIds = teamMember.SupplValues
                .Select(x => x.PropertyId)
                .Distinct()
                .ToList();

            var categories = await _categoryModule.GetAllAsync(propertyIds);
            var propertyMap = categories
            .SelectMany(category => category.Properties.Select(info => new
            {
                CategoryId = category.Id,
                CategoryName = category.Name,
                Property = info
            }))
            .ToDictionary(
                x => x.Property.Id,
                x => new
                {
                    x.CategoryId,
                    x.CategoryName,
                    Property = x.Property
                });

             var supplementaryValues = teamMember.SupplValues
            .GroupBy(value =>
            {
                if (!propertyMap.TryGetValue(value.PropertyId, out var property))
                {
                    throw new NotFoundException("Property not found");
                }

                return new
                {
                    property.CategoryId,
                    property.CategoryName
                };
            })
            .Select(group => new CategoryTmResponse
            {
                CategoryId = group.Key.CategoryId,
                CategoryName = group.Key.CategoryName,
                Informations = group.Select(value =>
                {
                    var property = propertyMap[value.PropertyId];
                    return new SupplementaryDataTm
                    {
                        PropertyId = property.Property.Id,
                        PropertyName = property.Property.Name,
                        Value = value.Data
                    };
                }).ToList()
            })
            .ToList();

            return new ApiResponse<GetTeamMemberResponse>
            {
                Success = true,
                Message = "GET GetTeamMember",
                Code = 200,
                Data = new GetTeamMemberResponse
                {
                    Id = teamMember.Id,
                    Identity = new IdentityDto(
                        teamMember.Identity.LastName,
                        teamMember.Identity.FirstName,
                        teamMember.Identity.FullName,
                        teamMember.Identity.Email,
                        teamMember.Identity.Position
                    ),
                    Image = teamMember.Image,
                    Role = teamMember.Roles.Count() > 0 ? teamMember.Roles[0].Role.Title.Value : "Utilisateur",
                    LastLoginAt = teamMember.LastLoginAt,
                    SupplementaryData = supplementaryValues
                }
            };
        }
    }
}