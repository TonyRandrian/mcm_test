using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using Mcm.Company.Infrastructure.Database;
using Mcm.Shared.Application.Modules;
using Mcm.Shared.Application.Modules.DTOs;
using Mcm.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Company.Infrastructure.Repositories
{
    public class CompanyModule(CompanyDbContext context, IPropertyModule propertyModule)
        : ICompanyModule
    {
        private readonly CompanyDbContext _context = context;
        private readonly IPropertyModule _propertyModule = propertyModule;

        public async Task<CompanyDto> CreateAsync(string Name, string Acronym, string Description, Resource Logo, List<ValueRequest>? Values)
        {
            var company = Domain.Entities.Company.Create(
                Name,
                Acronym,
                Description
            );
            
            company.UpdateLogo(Logo);
            company.TenantId = Guid.NewGuid();
            await _context.Companies.AddAsync(company);
            
            foreach (var value in Values ?? [])
            {
                var property = await _propertyModule.GetByIdAsync(value.PropertyId);
                if (property is not null)
                {
                    company.AddValue(value.Value, property.Id, property.IsMultiple, property.IsSensitive);
                }
            }
            
            await _context.SaveChangesAsync();
            return new CompanyDto(
                company.Id, company.Name, company.Acronym, company.IsContact, company.TenantId);
        }

        public async Task<CompanyDto?> FindCompanyByIdOutTenant(Guid id)
        {
            return await _context.Companies
                .AsNoTracking()
                .IgnoreQueryFilters(["MultiTenant"])
                .Where(c => c.Id == id)
                .Select(c => new CompanyDto(
                    c.Id, c.Name, c.Acronym, c.IsContact, c.TenantId))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CompanyDto>> GetAllCompanies(List<Guid> companies)
        {
            return await _context.Companies
                .AsNoTracking()
                .Where(c => companies.Contains(c.Id))
                .Select(c => new CompanyDto(
                    c.Id, c.Name, c.Acronym, c.IsContact, c.TenantId))
                .ToListAsync();
        }

        public async Task<CompanyDto?> GetCompanyById(Guid id)
        {
            return await _context.Companies
                .AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new CompanyDto(
                    c.Id, c.Name, c.Acronym, c.IsContact, c.TenantId))
                .FirstOrDefaultAsync();
        }

        public async Task<Dictionary<Guid, TypeContactDto>> GetExistTypeContact()
        {
            var types = await _context.TypeContacts
                .ToDictionaryAsync(
                    c => c.Id, 
                    c => new
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Color = c.Color,
                    }
                );

            return _context.Companies
                .Where(c => c.IsContact && c.TypeContactId != null)
                .ToDictionary(c => c.Id, c => new TypeContactDto(
                    types[c.TypeContactId ?? Guid.Empty].Id, types[c.TypeContactId ?? Guid.Empty].Name, types[c.TypeContactId ?? Guid.Empty].Color
            ));
        }

        public async Task<bool> HasLeader(Guid companyId)
        {
            return await _context.Companies.AnyAsync(
                c => c.Id == companyId && c.LeaderId != null);
        }
    }
}