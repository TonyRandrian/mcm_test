using Mcm.Shared.Application.Modules.DTOs;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Shared.Application.Modules
{
    public interface ICompanyModule
    {
        Task<CompanyDto?> GetCompanyById(Guid id);
        Task<CompanyDto?> FindCompanyByIdOutTenant(Guid id);
        Task<IEnumerable<CompanyDto>> GetAllCompanies(List<Guid> companies);
        Task<CompanyDto> CreateAsync(
            string Name, string Acronym, string Description, Resource Logo, List<ValueRequest>? values);
        Task<bool> HasLeader(Guid companyId);
        Task<Dictionary<Guid, TypeContactDto>> GetExistTypeContact();
    }
}