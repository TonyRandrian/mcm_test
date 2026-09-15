using Mcm.Authorizations.Application.Interfaces;
using Mcm.Authorizations.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using Mcm.Shared.Application.Services;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;

namespace Mcm.Authorizations.Application.Features.TeamMembers.Queries.GetCurrentTeamMember
{
    public class GetCurrentTeamMemberQueryHandler(
        ICurrentUserService currentUserService,
        ITeamMemberRepository teamMemberRepository,
        IRoleRepository roleRepository,
        ICompanyModule companyModule,
        ICategoryModule categoryModule,
        IPermissionService permissionService)
        : IRequestHandler<GetCurrentTeamMemberQuery, ApiResponse<GetCurrentTeamMemberResponse>>
    {
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly ITeamMemberRepository _teamMemberRepository = teamMemberRepository;
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly ICompanyModule _companyModule = companyModule;
        private readonly ICategoryModule _categoryModule = categoryModule;
        private readonly IPermissionService _permissionService = permissionService;
        
        public async Task<ApiResponse<GetCurrentTeamMemberResponse>> Handle(GetCurrentTeamMemberQuery request, CancellationToken cancellationToken)
        {
            var tm = await _teamMemberRepository.GetByIdAsync(_currentUserService.TeamMemberId)
                ?? throw NotFoundException.NotFoundById(nameof(TeamMember), _currentUserService.TeamMemberId);

            var roles = await _roleRepository.GetAllAsync(
                includes: [r => r.RoleCompanies],
                predicate: r => r.Members.Any(m => m.TeamMemberId == tm.Id),
                ct: cancellationToken);

            var companyIds = roles
                .SelectMany(r => r.RoleCompanies)
                .Select(rc => rc.CompanyId)
                .Distinct()
                .ToList();
            var companies = companyIds.Count > 0
                ? await _companyModule.GetAllCompanies(companyIds)
                : [];

            var perms = await _permissionService.GetActions(tm.Id, _currentUserService.CompanyId);
            
            var propertyIds = tm.SupplValues
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
            var supplementaryValues = tm.SupplValues
            .GroupBy(value =>
            {
                if (!propertyMap.TryGetValue(value.PropertyId, out var property))
                    throw new NotFoundException("Property not found");
                return new
                {
                    property.CategoryId,
                    property.CategoryName
                };
            })
            .Select(group => new TeamMemberCategoryDataResponse
            {
                CategoryId = group.Key.CategoryId,
                CategoryName = group.Key.CategoryName,
                Informations = group.Select(value =>
                {
                    var property = propertyMap[value.PropertyId];
                    return new TeamMemberPropertyData
                    {
                        PropertyId = property.Property.Id,
                        PropertyName = property.Property.Name,
                        IsSensitive = property.Property.IsSensitive,
                        Value = value.Data
                    };
                }).ToList()
            })
            .ToList();

            return new ApiResponse<GetCurrentTeamMemberResponse>
            {
                Success = true,
                Code = 200,
                Message = "Current User Details successfully",
                Data = new GetCurrentTeamMemberResponse
                {
                    LastName = tm.Identity.LastName,
                    FirstName = tm.Identity.FirstName,
                    Email = tm.Identity.Email,
                    Position = tm.Identity.Position,
                    Permissions = perms
                        .Select(p => new GetCurrentTeamMember_Permission(p.Module, p.Action))
                        .ToList(),
                    Companies = companies
                        .Select(c => new GetCurrentTeamMember_Company(c.Id, c.Name.Value, c.Acronym))
                        .ToList(),
                    SupplementaryData = supplementaryValues
                }
            };
        }
    }
}