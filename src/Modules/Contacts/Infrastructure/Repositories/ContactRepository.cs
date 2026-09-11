using Mcm.Company.Application.Interfaces;
using Mcm.Contacts.Application.Interfaces;
using Mcm.Contacts.Domain.Entities;
using Mcm.Contacts.Infrastructure.Database;
using Mcm.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Contacts.Infrastructure.Repositories
{
    public class ContactRepository(ContactDbContext context)
        : GenericRepository<Contact>(context), IContactRepository
    {
    }
}