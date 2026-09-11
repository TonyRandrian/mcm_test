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
                    company.AddValue(value.Value, property.Id, property.IsMultiple);
                }
            }
            
            await _context.SaveChangesAsync();
            return new CompanyDto(
                company.Id, company.Name, company.IsContact, company.TenantId);
        }

        public async Task<CompanyDto?> FindCompanyByIdOutTenant(Guid id)
        {
            return await _context.Companies
                .AsNoTracking()
                .IgnoreQueryFilters(["MultiTenant"])
                .Where(c => c.Id == id)
                .Select(c => new CompanyDto(
                    c.Id, c.Name, c.IsContact, c.TenantId))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CompanyDto>> GetAllCompanies(List<Guid> companies)
        {
            return await _context.Companies
                .AsNoTracking()
                .Where(c => companies.Contains(c.Id))
                .Select(c => new CompanyDto(
                    c.Id, c.Name, c.IsContact, c.TenantId))
                .ToListAsync();
        }

        public async Task<CompanyDto?> GetCompanyById(Guid id)
        {
            return await _context.Companies
                .AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new CompanyDto(
                    c.Id, c.Name, c.IsContact, c.TenantId))
                .FirstOrDefaultAsync();
        }

        public async Task<Dictionary<Guid, TypeContactDto>> GetExistTypeContact()
        {
            var company = await _context.Companies
                .Where(c => c.IsContact)
                .Select(c => c)
                .ToListAsync();

            return company.ToDictionary(c => c.Id, c => new TypeContactDto(
                c.TypeContactId ?? Guid.Empty, c.TypeContact?.Name ?? null!, c.TypeContact?.Color ?? null!
            ));
        }

        public async Task<bool> HasLeader(Guid companyId)
        {
            return await _context.Companies.AnyAsync(
                c => c.Id == companyId && c.LeaderId != null);
        }
    }
}