using Mcm.Contacts.Application.Interfaces;
using Mcm.Contacts.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using MediatR;

namespace Mcm.Contacts.Application.Features.Contacts.Queries.GetContact
{
    public class GetContactQueryHandler(
        IContactRepository contactRepository,
        ICategoryModule categoryModule,
        ICompanyModule companyModule)
        : IRequestHandler<GetContactQuery, ApiResponse<GetContactResponse>>
    {
        private readonly IContactRepository _contactRepository = contactRepository;
        private readonly ICategoryModule _categoryModule = categoryModule;
        private readonly ICompanyModule _companyModule = companyModule;

        public async Task<ApiResponse<GetContactResponse>> Handle(GetContactQuery query, CancellationToken cancellationToken)
        {
            var contact = await _contactRepository.GetByIdAsync(query.Request.Id)
                    ?? throw NotFoundException.NotFoundById(nameof(Contact), query.Request.Id);

            var propertyIds = contact.Values
                .Select(x => x.PropertyId)
                .Distinct()
                .ToList();

            var companies = await _categoryModule.GetAllAsync(propertyIds);
            var propertyMap = companies
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
            var supplementaryValues = contact.Values
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
            .Select(group => new CategoryContactResponse
            {
                CategoryId = group.Key.CategoryId,
                CategoryName = group.Key.CategoryName,
                Informations = group.Select(value =>
                {
                    var property = propertyMap[value.PropertyId];
                    return new SupplementaryDataContact
                    {
                        PropertyId = property.Property.Id,
                        PropertyName = property.Property.Name,
                        Value = value.Data
                    };
                }).ToList()
            })
            .ToList();

            var company = await _companyModule.GetCompanyById(contact.CompanyId)
                ?? throw NotFoundException.NotFoundById("Company", contact.CompanyId);

            return new ApiResponse<GetContactResponse>
            {
                Success = true,
                Message = "GET GetTeamMember",
                Code = 200,
                Data = new GetContactResponse
                {
                    Id = contact.Id,
                    LastName = contact.Identity.LastName,
                    FirstName = contact.Identity.FirstName,
                    Email = contact.Identity.Email,
                    Poste = contact.Identity.Position,
                    Image = contact.Image,
                    CIN = contact.Cin,
                    Phone = contact.Phone,
                    Company = new CompanyContactResponse(company.Id, company.Name),
                    SupplementaryValues = supplementaryValues
                }
            };
        }
    }
}