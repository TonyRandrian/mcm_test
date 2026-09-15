using Mcm.Company.Domain.Events;
using Mcm.Property.Domain.Enums;
using Mcm.Shared.Domain.Events;
using Mcm.Shared.Domain.Exceptions;
using Mcm.Shared.Domain.Extensions;
using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Domain.Primitives;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Company.Domain.Entities
{
    public class Company
        : AggregateRoot, ITenantScoped
    {
        public Name Name { get; private set; } = null!;
        public string Acronym
        {
            get;
            private set;
        } = string.Empty;
        public string Description 
        { 
            get; 
            private set  => field = value.ToCapitalize(); 
        } = string.Empty;
        public Resource? Logo { get; set; } = null!;
        public bool IsContact { get; set; }

        public Guid? LeaderId { get; set; }
        public Guid TenantId { get; set; }
        public Guid? ParentId { get; private set; }
        public Guid? TypeContactId { get; private set; }
        public Guid? CompanyId { get; private set; }

        public TypeContact? TypeContact { get; private set; } = null!;
        public Company? Parent { get; private set; } = null!;
        
        private List<CompanyValue> _values = [];
        public readonly List<CompanyActivity> _companyActivities = [];
        public IReadOnlyList<CompanyValue> SupplValues => _values;
        public IReadOnlyList<CompanyActivity> CompanyActivities => _companyActivities;

        private Company() { }
        private Company(string name, string acronym, string description, Guid? parentId, bool isContact = false, Guid? leaderId = null, Guid? typeContactId = null, Guid? companyId = null)
        {
            Name = name;
            Acronym = acronym;
            Description = description;
            ParentId = parentId;
            IsContact = isContact;
            LeaderId = leaderId;
            TypeContactId = typeContactId;
            CompanyId = companyId;
        }

        public static Company Create(string name, string acronym, string description, Guid? parentId = null, bool isContact = false, Guid? leaderId = null, Guid? typeContactId = null, Guid? companyId = null)
        {
            Company company = new(
                name, 
                acronym, 
                description, 
                parentId, 
                isContact, 
                leaderId, 
                typeContactId,
                companyId);
            if (company.ParentId is not null && !company.IsContact)
                company.RaiseDomainEvent(new SubsidiaryCreatedEvent(company.Id, company.Name));
            if (company.IsContact)
                company.RaiseDomainEvent(new CompanyContactCreatedEvent(company.Id, company.Name));
            return company;
        }

        public void Update(string? name, string? acronym, string? description)
        {
            if (name is not null) Name = name;
            if (acronym is not null) Acronym = acronym;
            if (description is not null) Description = description;
            if (Parent is null)
                RaiseDomainEvent(new CompanyUpdatedEvent(Id, Name));
            if (Parent is not null && !IsContact)
                RaiseDomainEvent(new SubsidiaryUpdatedEvent(Id, Name));
            if (IsContact)
                RaiseDomainEvent(new CompanyContactUpdatedEvent(Id, Name));
        }

        public void ConvertTypeContact()
        {
            if (TypeContact?.TypeConvertTo is null)
                return;
            TypeContactId = TypeContact.TypeConvertTo;
            SetUpdatedAt();
            RaiseDomainEvent(new CompanyContactUpdatedEvent(Id, Name));
        }

        public void UpdateLogo(Resource logo)
        {
            Logo = logo;
        }

        public void UpdateLeader(Guid leaderId)
        {
            if (ParentId is not null)
                RaiseDomainEvent(new LeaderDefinedEvent(leaderId, Id, Name.Value));
            LeaderId = leaderId;
        }
        
        public void AddValue(string data, Guid propertyId, bool isMultiple, bool isSensitive)
        {
            if (_values.Any(v => v.PropertyId == propertyId && !isMultiple))
                return;
            
            var value = CompanyValue.Create(
                Id, 
                propertyId, 
                isSensitive ? data.SetSensitive() : data, 
                TenantId);
            _values.Add(value);
        }

        public void AddMultipleValue(List<(string data, Guid propertyId, bool isMultiple, bool isSensitive)> values)
        {
            var hashValues = values.ToHashSet();
            List<CompanyValue> companyValues = [];
            foreach (var (data, propertyId, isMultiple, isSensitive) in values)
            {
                if (_values.Any(v => v.PropertyId == propertyId && !isMultiple))
                    continue;
                
                companyValues.Add(CompanyValue.Create(
                    Id, 
                    propertyId, 
                    isSensitive
                        ? data.SetSensitive()
                        : data,
                    TenantId));
            }
            _values.AddRange(companyValues);
        }

        public void UpdateValue(string data, Guid propertyId)
        {
            var exists = _values.FirstOrDefault(v => v.PropertyId == propertyId);
            if (exists is not null)
                exists.Data = data;
            else
                _values.Add(CompanyValue.Create(Id, propertyId, data, TenantId));
        }
 
        public void RemoveAllValuesByProperty(Guid propertyId)
        {
            _values.RemoveAll(v => v.PropertyId == propertyId);
        }
 
        public void RemoveValue(Guid companyValueId)
        {
            var founded = _values.FirstOrDefault(v => v.Id == companyValueId)
                ?? throw new DomainException("Value not found");
            _values.Remove(founded);
        }
 
        public void RemoveValueByPropertyId(Guid propertyId)
        {
            var founded = _values.FirstOrDefault(v => v.PropertyId == propertyId)
                ?? throw new DomainException("Value not found");
            _values.Remove(founded);
        }


        public void AddActivity(Guid activityId)
        {
            if (_companyActivities.Any(a => a.ActivitySectorId == activityId))
                return;
            var companyActivity = CompanyActivity.Create(Id, activityId, TenantId);
            _companyActivities.Add(companyActivity);
        }

        public void SynchActivities(List<Guid> activityIds)
        {
            var activityHash = activityIds.ToHashSet();
            _companyActivities.RemoveAll(a => !activityHash.Contains(a.ActivitySectorId));
            foreach (var item in activityHash.ToList())
            {
                this.AddActivity(item);
            }
        }

        public override void Delete()
        {
            base.Delete();
            if (ParentId is null)
                RaiseDomainEvent(new CompanyDeletedEvent(Id, Name));
            if (ParentId is not null && !IsContact)
                RaiseDomainEvent(new SubsidiaryDeletedEvent(Id, Name));
            if (IsContact)
                RaiseDomainEvent(new CompanyContactDeletedEvent(Id, Name));
        }

        public override bool Equals(object? obj)
        {
            return obj is not null
                && obj is Company other
                && Name.Equals(other.Name)
                && Acronym.Equals(other.Acronym)
                && Description.Equals(other.Description)
                && ParentId == other.ParentId
                && IsContact == other.IsContact;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Acronym, Description, ParentId, IsContact);
        }
    }
}