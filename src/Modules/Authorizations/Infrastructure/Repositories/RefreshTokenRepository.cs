using System.Linq.Expressions;
using Mcm.Authorizations.Application.Interfaces;
using Mcm.Authorizations.Domain.Entities;
using Mcm.Authorizations.Infrastructure.Database;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Authorizations.Infrastructure.Repositories
{
    public class RefreshTokenRepository(AuthorizationDbContext context)
        : GenericRepository<RefreshToken>(context), IRefreshTokenRepository
    {
        public async Task<RefreshToken?> GetRefreshTokenAsync(string refreshToken)
        {
            return await _dbSet.FirstOrDefaultAsync(rt => rt.Token == refreshToken);
        }
    }
}