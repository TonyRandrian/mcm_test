using Mcm.Authorizations.Domain.Events;
using Mcm.Shared.Domain.Exceptions;
using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Domain.Primitives;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Authorizations.Domain.Entities
{
    public class TeamMember : AggregateRoot, ITenantScoped
    {
        public Identity Identity { get; private set; } = null!;
        public string HashedPassword { get; private set; } = string.Empty;
        public string? InvitationToken { get; private set; } = string.Empty;
        public DateTime LastLoginAt { get; set; }

        public Guid CompanyId { get; private set; }
        public Guid TenantId { get; set; }

        public Resource? Image { get; set; } = null!;
        private List<TeamMemberValue> _values = [];
        public IReadOnlyList<TeamMemberValue> SupplValues => _values;
        private List<RoleTeamMember> _roles = [];
        public IReadOnlyList<RoleTeamMember> Roles => _roles;

        private TeamMember() { }

        private TeamMember(Identity identity, Guid companyId, Guid? tenantId)
        {
            Identity = identity;
            CompanyId = companyId;
            TenantId = tenantId ?? Guid.Empty;
        }

        public static TeamMember Create(Identity identity, Guid companyId, Guid? tenantId = null)
        {
            return new TeamMember(identity, companyId, tenantId);
        }

        public void Update(Identity? identity)
        {
            if (identity is not null)
                Identity = identity;
            SetUpdatedAt();
        }

        public bool IsInvitationAccepted => InvitationToken is null || InvitationToken == string.Empty;

        public void SetInvitationToken(string token)
        {
            InvitationToken = token;
            SetUpdatedAt();
            RaiseDomainEvent(new TeamMemberInvited(Id, Identity, CompanyId, token));
        }

        public void InvitationAccepted()
        {
            InvitationToken = null;
            SetUpdatedAt();
        }

        public void SetPassword(string hashedPassword)
        {
            HashedPassword = hashedPassword;
            SetUpdatedAt();
        }

        public void AddValue(string data, Guid propertyId, bool isMultiple)
        {
            if (_values.Any(v => v.PropertyId == propertyId && !isMultiple))
                return;
            _values.Add(TeamMemberValue.Create(Id, propertyId, data, TenantId));
        }

        public void RemoveValue(Guid propertyId)
        {
            var founded = _values.FirstOrDefault(v => v.PropertyId == propertyId)
                ?? throw new DomainException("Value not found");
            _values.Remove(founded);
        }

        public void Connect() => LastLoginAt = DateTime.UtcNow;
        public void SetImage(Resource image) => Image = image;
        public void UpdateValue(string data, Guid propertyId)
        {
            var exists = _values.FirstOrDefault(v => v.PropertyId == propertyId);
            if (exists is not null)
                exists.Data = data;
            else
                _values.Add(TeamMemberValue.Create(Id, propertyId, data, TenantId));
        }
 
        public void RemoveAllValuesByProperty(Guid propertyId)
        {
            _values.RemoveAll(v => v.PropertyId == propertyId);
        }
    }
}