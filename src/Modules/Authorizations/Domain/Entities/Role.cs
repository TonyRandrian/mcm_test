using System.Reflection;
using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Domain.Exceptions;
using Mcm.Shared.Domain.Extensions;
using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Domain.Primitives;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Authorizations.Domain.Entities
{
    public class Role : AuditableEntity, ITenantScoped
    {
        public Name Title { get; private set; } = null!;
        public string Description
        {
            get;
            private set => field = value.ToCapitalize(); 
        } = string.Empty;
        public bool IsSystem { get; private set; }
        public Guid TenantId { get; set; }

        private readonly List<RoleTeamMember> _members = [];
        private readonly List<Permission> _authorizations = [];
        private readonly List<RoleCompany> _roleCompanies = [];
        public IReadOnlyList<RoleTeamMember> Members => _members.AsReadOnly();
        public IReadOnlyList<Permission> Authorizations => _authorizations.AsReadOnly();
        public IReadOnlyList<RoleCompany> RoleCompanies => _roleCompanies.AsReadOnly();

        private Role() { }
        private Role(string title, string description, bool isSystem)
        {
            Title = title;
            Description = description;
            IsSystem = isSystem;
        }

        public static Role Create(string title, string description, bool isSystem = false)
            => new(title, description, isSystem);

        public void Update(string? title, string? description)
        {
            if (title is not null)
                Title = title;
            if (description is not null)
                Description = description;
        }

        public void AddPermission(PermModule module, PermAction action)
        {
            var exist = _authorizations.Any(a => 
                a.Module.Equals(module)
                && a.Action.Equals(action));
            if (exist)
                return;
            _authorizations.Add(Permission.Create(Id, module, action, TenantId));
        }
        public void SyncPermissions(IEnumerable<(PermModule, PermAction)> perms)
        {
            var permHash = perms.ToHashSet();
            _authorizations.RemoveAll(a => !permHash.Contains((a.Module, a.Action)));
            foreach (var (module, action) in perms.ToList())
            {
                this.AddPermission(module, action);           
            }
        }

        public void AddCompany(Guid companyId)
        {
            var exist = _roleCompanies.Any(a => 
                a.CompanyId == companyId);
            if (exist)
                return;
            _roleCompanies.Add(RoleCompany.Create(Id, companyId, TenantId));
        }
        public void SyncCompanies(IEnumerable<Guid> companies)
        {
            var companiesHash = companies.ToHashSet();
            _roleCompanies.RemoveAll(c => !companiesHash.Contains(c.CompanyId));
            foreach (var company in companiesHash.ToList())
            {
                this.AddCompany(company);           
            }
        }

        public void AddMember(Guid tmId)
        {
            var exist = _members.Any(a => 
                a.TeamMemberId == tmId);
            if (exist)
                return;
            _members.Add(RoleTeamMember.Create(Id, tmId, TenantId));
        }
    }
}