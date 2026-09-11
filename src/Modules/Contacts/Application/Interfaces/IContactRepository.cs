using Mcm.Contacts.Domain.Entities;
using Mcm.Shared.Application.Interfaces;

namespace Mcm.Contacts.Application.Interfaces
{
    public interface IContactRepository
        : IGenericRepository<Contact>
    {
    }
}