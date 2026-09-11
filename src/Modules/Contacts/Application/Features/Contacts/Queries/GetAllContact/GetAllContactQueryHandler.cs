using System.Text.Json;
using Mcm.Contacts.Application.Interfaces;
using Mcm.Contacts.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using Mcm.Shared.Application.Modules.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Contacts.Application.Features.Contacts.Queries.GetAllContact
{
    public class GetAllContactQueryHandler(
        IContactRepository contactRepository,
        ICompanyModule companyModule,
        ICategoryModule categoryModule,
        ICurrentUserService currentUserService)
        : IRequestHandler<GetAllContactQuery, ApiResponse<GetAllContactResponse>>
    {
        private static readonly HashSet<string> _knownFields =
            ["lastname", "firstname", "email", "poste", "cin", "phone", "note", "image", "company"];

        public async Task<ApiResponse<GetAllContactResponse>> Handle(
            GetAllContactQuery query, CancellationToken cancellationToken)
        {
            var projection = query.Request.Projection;

            var requestedFields = projection?.Fields.Count > 0
                ? projection.Fields.Select(f => f.ToLowerInvariant()).ToHashSet()
                : _knownFields;

            var requestedPropertyIds = projection?.PropertyIds.ToHashSet() ?? [];
            bool needsCompany    = requestedFields.Contains("company");
            bool needsValues     = requestedPropertyIds.Count > 0;

            Func<IQueryable<Contact>, IQueryable<Contact>>? selector = needsValues
                ? q => q.Include(c => c.Values
                            .Where(v => requestedPropertyIds.Contains(v.PropertyId)))
                : null;

            var contacts = await contactRepository.GetAllAsync(
                predicate: c =>
                    c.CompanyId == currentUserService.CompanyId
                    && (string.IsNullOrEmpty(query.Request.Search)
                        || c.Identity.LastName.Value.ToLower().Contains(query.Request.Search.ToLower())),
                orderBy: q => q.OrderByDescending(c => c.CreatedAt),
                selector: selector,
                pageQuery: new PageQuery(query.Request.Page, query.Request.Limit),
                ct: cancellationToken
            );

            Dictionary<Guid, CompanyDto> companyMap = [];
            if (needsCompany)
            {
                var companyIds = contacts.Select(c => c.AssociatedCompanyId).Distinct().ToList();
                var companies  = await companyModule.GetAllCompanies(companyIds);
                companyMap     = companies.ToDictionary(c => c.Id);
            }

            Dictionary<Guid, string> propertyNames = [];
            if (needsValues)
            {
                var categories  = await categoryModule.GetAllAsync(requestedPropertyIds.ToList());
                propertyNames   = categories
                    .SelectMany(c => c.Properties)
                    .Where(p => requestedPropertyIds.Contains(p.Id))
                    .ToDictionary(p => p.Id, p => p.Name.Value);
            }

            var typeContactsCompany  = await companyModule.GetExistTypeContact();
            Console.WriteLine(JsonSerializer.Serialize(typeContactsCompany));
            
            var data = contacts
                .Select(c => Project(c, requestedFields, requestedPropertyIds, companyMap, propertyNames, typeContactsCompany))
                .ToList();

            return new ApiResponse<GetAllContactResponse>
            {
                Success = true,
                Message = "Contacts retrieved successfully",
                Code = 200,
                Data = new GetAllContactResponse(data),
                Meta = new Meta
                {
                    Page  = query.Request.Page,
                    Limit = query.Request.Limit,
                    Total = await contactRepository.CountAsync()
                }
            };
        }

        private static ContactResponse Project(
            Contact contact,
            HashSet<string> fields,
            HashSet<Guid> propertyIds,
            Dictionary<Guid, CompanyDto> companyMap,
            Dictionary<Guid, string> propertyNames,
            Dictionary<Guid, TypeContactDto> typeContacts)
        {
            var response = new ContactResponse { Id = contact.Id };

            if (fields.Contains("lastname"))  response.LastName  = contact.Identity.LastName;
            if (fields.Contains("firstname")) response.FirstName = contact.Identity.FirstName;
            if (fields.Contains("email"))     response.Email     = contact.Identity.Email;
            if (fields.Contains("poste"))     response.Poste     = contact.Identity.Position;
            if (fields.Contains("cin"))       response.CIN       = contact.Cin;
            if (fields.Contains("phone"))     response.Phone     = contact.Phone;
            if (fields.Contains("image"))     response.Image     = contact.Image;

            if (fields.Contains("company") && companyMap.TryGetValue(contact.AssociatedCompanyId, out var company))
                response.Company = new CompanyContactsResponse(company.Id, company.Name);

            if (propertyIds.Count > 0)
            {
                response.Values = contact.Values
                    .Where(v => propertyIds.Contains(v.PropertyId))
                    .Select(v => new ProjectedValue
                    {
                        PropertyId   = v.PropertyId,
                        PropertyName = propertyNames.GetValueOrDefault(v.PropertyId, "Unknown"),
                        Value         = v.Data
                    })
                    .ToList();
            }

           if (typeContacts.TryGetValue(contact.AssociatedCompanyId, out var type))
                response.TypeContact = new TypeContactResponse
                {
                    Id = type.Id,
                    Name = type.Name.Value,
                    Color = type.Color
                };

            return response;
        }
    }
}