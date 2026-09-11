using Mcm.Authorizations.Application.Interfaces;
using Mcm.Authorizations.Infrastructure.Database;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Infrastructure.Repositories;

namespace Mcm.Authorizations.Infrastructure.Repositories
{
    public class AuthorizationUow(AuthorizationDbContext context)
        : UnitOfWork<AuthorizationDbContext>(context), IAuthorizationUow
    {
        
    }
}