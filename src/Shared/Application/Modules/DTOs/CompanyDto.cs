using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Shared.Application.Modules.DTOs
{
    public record CompanyDto
    (
        Guid Id,
        Name Name,
        bool IsContact,
        Guid TenantId
    );
}