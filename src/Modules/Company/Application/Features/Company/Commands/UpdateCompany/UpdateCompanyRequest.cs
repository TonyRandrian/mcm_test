using Microsoft.AspNetCore.Http;

namespace Mcm.Company.Application.Features.Company.Commands.UpdateCompany
{
    public record UpdateCompanyRequest
    (
        string? Name,
        string? Acronym,
        string? Description,
        IFormFile? Logo,
        List<Guid>? ActivitySectors
    );
}