using System.Data.Common;
using Mcm.Contacts.Application.Interfaces;
using Mcm.Contacts.Infrastructure.Database;
using Mcm.Shared.Application.Modules;
using Mcm.Shared.Application.Modules.DTOs;
using Mcm.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Contacts.Infrastructure.Repositories
{
    public class ContactModule(ContactDbContext context)
        : IContactModule
    {
        private readonly ContactDbContext _context = context;
        public async Task<List<IdentityDto>> GetIdentities(List<Guid> contactIds)
        {
            return await _context.Contacts.AsNoTracking()
                .Where(c => contactIds.Contains(c.Id))
                .OrderBy(c => c.Identity.LastName.Value)
                .Select(c => new IdentityDto(
                    c.Id, 
                    c.Identity.LastName, 
                    c.Identity.FirstName, 
                    (c.Image != null)? c.Image.Url : string.Empty,
                    c.Identity.Email))
                .ToListAsync();
        }
    }
}