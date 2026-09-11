using Mcm.Company.Application.Interfaces;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using Mcm.Shared.Application.Modules.DTOs;
using MediatR;

namespace Mcm.Company.Application.Features.Company.Queries.GetCompanyById
{
    public class GetCompanyQueryHandler(ICompanyRepository companyRepository, ICategoryModule categoryModule, ITypeContactRepository typeContactRepository)
        : IRequestHandler<GetCompanyByIdQuery, ApiResponse<GetCompanyByIdResponse>>
    {
        private readonly ICompanyRepository _companyRepository = companyRepository;
        private readonly ICategoryModule _categoryModule = categoryModule;
        private readonly ITypeContactRepository _typeContactRepository = typeContactRepository;

        public async Task<ApiResponse<GetCompanyByIdResponse>> Handle(GetCompanyByIdQuery query, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(query.Request.Id)
                    ?? throw NotFoundException.NotFoundById(nameof(Domain.Entities.Company), query.Request.Id);
            
            Domain.Entities.Company? parent = null;
            if (company.ParentId is not null)
                parent = await _companyRepository.GetByIdAsync(company.ParentId.Value)
                    ?? throw NotFoundException.NotFoundById(nameof(Domain.Entities.Company), company.ParentId.Value);

            var propertyIds = company.SupplValues
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

            var supplementaryValues = company.SupplValues
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
            .Select(group => new CategoryByIdResponse
            (
                CategoryId: group.Key.CategoryId,
                CategoryName: group.Key.CategoryName,
                Informations: group.Select(value =>
                {
                    var property = propertyMap[value.PropertyId];

                    return new CompanyValue
                    (
                        PropertyId: property.Property.Id,
                        PropertyName: property.Property.Name,
                        Value: value.Data
                    );
                })
            ))
            .ToList();

            Domain.Entities.TypeContact? typeContactTo = null;
            if (company.TypeContact?.TypeConvertTo is not null && company.TypeContact is not null)
                typeContactTo = await _typeContactRepository.GetByIdAsync(company.TypeContact.TypeConvertTo.Value)
                    ?? throw NotFoundException.NotFoundById(nameof(Domain.Entities.TypeContact), company.TypeContact.TypeConvertTo.Value);

            return new ApiResponse<GetCompanyByIdResponse>
            {
                Success = true,
                Message = "Company retrieved successfully",
                Code = 200,
                Data = new GetCompanyByIdResponse
                {
                    Id = company.Id,
                    Name = company.Name,
                    Acronym = company.Acronym,
                    Description = company.Description,
                    Logo = company.Logo,
                    CreatedAt = company.CreatedAt,
                    Parent = (parent is not null)
                        ? new ParentCompany(parent.Id, parent.Name)
                        : null,
                    ActivitySectors = company.CompanyActivities?
                        .Select(ca => new CompanyActivityDto(ca.ActivitySectorId, ca.ActivitySector.Name)),
                    TypeContact = company.TypeContact is not null ? new TypeContactDto(
                        Id: company.TypeContact.Id,
                        Name: company.TypeContact.Name,
                        Color: company.TypeContact.Color,
                        TypeConvertTo: typeContactTo is not null ? 
                            new TypeContactDto(
                                Id: typeContactTo.Id,
                                Name: typeContactTo.Name,
                                null)
                            : null
                    ) : null,
                    SupplementaryValues = supplementaryValues
                   
                }
            };

        }
    }
}