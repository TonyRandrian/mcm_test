using Mcm.Company.Application.Features.Dashboard;
using Mcm.Shared.Application.Interfaces;

namespace Mcm.Company.Application.Interfaces
{
    public interface ICompanyRepository
        : IGenericRepository<Domain.Entities.Company>
    {
        Task<Domain.Entities.Company?> FindCompanyByIdOutTenant(Guid id);
        Task<List<TypeContactViewData>> GetDashboardData(Guid companyId);
    }
}