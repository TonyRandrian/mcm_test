using Mcm.Company.Application.Interfaces;
using Mcm.Company.Domain.Entities;
using Mcm.Company.Infrastructure.Database;
using Mcm.Shared.Infrastructure.Repositories;

namespace Mcm.Company.Infrastructure.Repositories
{
    public class TypeContactRepository(CompanyDbContext context)
        : GenericRepository<TypeContact>(context), ITypeContactRepository
    {
    }
}