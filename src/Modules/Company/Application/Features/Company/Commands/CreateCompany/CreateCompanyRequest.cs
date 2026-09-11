using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mcm.Company.Application.Features.Company.Commands.CreateCompany
{
    public record CreateCompanyRequest
    (
        string Name,
        string Acronym,
        string Description,
        IFormFile? Logo,
        Guid? ParentId = null,
        bool IsContact = false,
        Guid? LeaderId = null,
        Guid? TypeContactId = null,
        List<Guid>? ActivitySectors = null,
        string? Values = null
    );


    public record CreateCompanyValue
    (
        string Value,
        Guid PropertyId
    );
    
}