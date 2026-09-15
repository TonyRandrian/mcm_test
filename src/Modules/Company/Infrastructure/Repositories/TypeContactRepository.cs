using Mcm.Company.Application.Interfaces;
using Mcm.Company.Domain.Entities;
using Mcm.Company.Infrastructure.Database;
using Mcm.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Company.Infrastructure.Repositories
{
    public class TypeContactRepository(CompanyDbContext context)
        : GenericRepository<TypeContact>(context), ITypeContactRepository
    {
        public override async Task<TypeContact?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Include(tc => tc.TypeConvertToNavigation)
                .FirstOrDefaultAsync(tc => tc.Id == id);
        }
    }
}