using Mcm.Company.Application.Interfaces;
using Mcm.Company.Infrastructure.Database;
using Mcm.Shared.Infrastructure.Repositories;

namespace Mcm.Company.Infrastructure.Repositories
{
    public class CompanyUow(CompanyDbContext context)
        : UnitOfWork<CompanyDbContext>(context), ICompanyUow
    {
    }
}