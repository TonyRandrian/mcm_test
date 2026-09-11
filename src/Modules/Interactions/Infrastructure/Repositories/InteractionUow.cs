using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Infrastructure.Database;
using Mcm.Shared.Infrastructure.Repositories;

namespace Mcm.Interactions.Infrastructure.Repositories
{
    public class InteractionUow(InteractionDbContext context)
        : UnitOfWork<InteractionDbContext>(context), IInteractionUow
    {
    }
}