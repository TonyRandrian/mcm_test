using Mcm.Authorizations.Application.Interfaces;
using Mcm.Authorizations.Domain.Entities;
using Mcm.Authorizations.Infrastructure.Database;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Authorizations.Infrastructure.Repositories
{
    public class RoleRepository(AuthorizationDbContext context)
        : GenericRepository<Role>(context), IRoleRepository
    {
    }
}