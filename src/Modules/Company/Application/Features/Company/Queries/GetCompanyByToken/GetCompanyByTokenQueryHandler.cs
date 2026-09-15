using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using Mcm.Company.Application.Interfaces;
using Mcm.Property.Domain.Enums;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using Mcm.Shared.Application.Modules.DTOs;
using MediatR;

namespace Mcm.Company.Application.Features.Company.Queries.GetCompanyByToken
{
    public class GetCompanyByTokenQueryHandler(ICompanyRepository companyRepository, ICategoryModule categoryModule, ICurrentUserService currentUserService) : IRequestHandler<GetCompanyByTokenQuery, ApiResponse<GetCompanyByTokenResponse>>
    {
        private readonly ICompanyRepository _companyRepository = companyRepository;
        private readonly ICategoryModule _categoryModule = categoryModule;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<ApiResponse<GetCompanyByTokenResponse>> Handle(GetCompanyByTokenQuery query, CancellationToken cancellationToken)
        {
            var Id = _currentUserService.CompanyId;
            var company = await _companyRepository.GetByIdAsync(Id)
                    ?? throw NotFoundException.NotFoundById(nameof(Domain.Entities.Company), Id);
            Domain.Entities.Company? parent = null;
            if (company.ParentId is not null)
                parent = await _companyRepository.GetByIdAsync(company.ParentId.Value)
                    ?? throw NotFoundException.NotFoundById(nameof(Domain.Entities.Company), company.ParentId.Value);

            var propertyIds = company.SupplValues
                .Select(x => x.PropertyId)
                .Distinct()
                .ToList();

            var categories = await _categoryModule.GetAllAsync(propertyIds);
            var visibleCategoryIds = await _categoryModule.GetVisibleCategoryIdsAsync(EntityType.Company);
            categories = [.. categories.Where(c => visibleCategoryIds.Contains(c.Id))];
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

            var supplementaryValues = company.SupplValues
            .Where(value => propertyMap.ContainsKey(value.PropertyId))
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
            .Select(group => new CategoryByTokenResponse
            {
                CategoryId = group.Key.CategoryId,
                CategoryName = group.Key.CategoryName,
                Informations = group.Select(value =>
                {
                    var property = propertyMap[value.PropertyId];

                    return new CompanyValueToken
                    {
                        PropertyId = property.Property.Id,
                        PropertyName = property.Property.Name,
                        IsSensitive = property.Property.IsSensitive,
                        Value = value.Data
                    };
                })
            })
            .ToList();

            return new ApiResponse<GetCompanyByTokenResponse>
            {
                Success = true,
                Message = "Company retrieved successfully",
                Code = 200,
                Data = new GetCompanyByTokenResponse
                {
                    Id = company.Id,
                    Name = company.Name,
                    Acronym = company.Acronym,
                    Description = company.Description,
                    Logo = company.Logo,
                    CreatedAt = company.CreatedAt,
                    Parent = (parent is not null)
                        ? new ParentCompanyToken{ Id = parent.Id, Name = parent.Name}
                        : null,
                    ActivitySectors = company.CompanyActivities?
                        .Select(ca => (ca.ActivitySectorId, ca.ActivitySector.Name)),
                    TypeContact = company.TypeContact is not null ? new TypeContactDTo
                    (
                        Id: company.TypeContact.Id,
                        Name: company.TypeContact.Name,
                        Description: company.TypeContact.Description,
                        TypeConvertTo: company.TypeContact.TypeConvertTo,
                        TypeConvertToName: company.TypeContact.TypeConvertToNavigation?.Name ?? string.Empty
                    ) : null,
                    SupplementaryValues = supplementaryValues
                }
            };
        }
    }
}