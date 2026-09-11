using Mcm.Shared.Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Shared.Infrastructure.Extensions
{
    public static class MediatorExtension
    {
        public static async Task DispatchDomainEventAsync(this IMediator mediator, DbContext context)
        {
            var domainEntities = context.ChangeTracker
                .Entries<AggregateRoot>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();
            
            domainEntities.ToList()
                .ForEach(x => x.Entity.ClearDomainEvent());

            foreach (var @event in domainEvents)
            {
                await mediator.Publish(@event);
            }

        }
    }
}