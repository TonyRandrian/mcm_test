using Mcm.Contacts.Application.Interfaces;
using Mcm.Contacts.Infrastructure.Database;
using Mcm.Shared.Infrastructure.Repositories;

namespace Mcm.Contacts.Infrastructure.Repositories
{
    public class ContactUow(ContactDbContext context)
        : UnitOfWork<ContactDbContext>(context), IContactUow
    {
    }
}