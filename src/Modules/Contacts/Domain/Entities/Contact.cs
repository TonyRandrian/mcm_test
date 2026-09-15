using System.Security.Cryptography.X509Certificates;
using Mcm.Contacts.Domain.Events;
using Mcm.Shared.Domain.Exceptions;
using Mcm.Shared.Domain.Extensions;
using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Domain.Primitives;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Contacts.Domain.Entities
{
    public class Contact
        : AggregateRoot, ITenantScoped
    {
        public Identity Identity { get; private set; } = null!;
        public Guid CompanyId { get; private set; }
        public Guid AssociatedCompanyId { get; private set; }
        public Guid TenantId { get; set; }
        public Resource? Image { get; private set; }
        public string? Cin { get; private set; }
        public string? Phone { get; private set; }
    
        private readonly List<ContactValue> _values = [];
        public IReadOnlyCollection<ContactValue> Values => _values;

        private Contact() { }
        private Contact(Identity identity, string? cin, string? phone, Guid associatedCompanyId, Guid companyId)
        {
            Identity = identity;
            Cin = cin;
            Phone = phone;
            AssociatedCompanyId = associatedCompanyId;
            CompanyId = companyId;
        }

        public static Contact Create(Identity identity, string? cin, string? phone, Guid associatedCompanyId, Guid companyId)
        {
            Contact contact = new(identity, cin, phone, associatedCompanyId, companyId);
            contact.RaiseDomainEvent(new ContactCreatedEvent(contact.Id, contact.Identity.FullName));
            return contact;
        }

        public void Update(Identity? identity, string? cin, string? phone, Guid? associatedCompanyId)
        {
            if (identity is not null) ChangeIdentity(identity);
            if (cin is not null) ChangeInformation(cin, this.Phone ?? string.Empty);
            if (phone is not null) ChangeInformation(this.Cin ?? string.Empty, phone);
            if (associatedCompanyId.HasValue) ChangeAssociatedCompany(associatedCompanyId.Value);
            SetUpdatedAt();
            RaiseDomainEvent(new ContactUpdatedEvent(Id, Identity.FullName));
        }

        public override void Delete()
        {
            base.Delete();
            RaiseDomainEvent(new ContactDeletedEvent(Id, Identity.FullName));
        }

        public void UpdateImage(Resource image)
            => Image = image;

        public void AddValue(string data, Guid propertyId, bool isMultiple, bool isSensitive)
        {
            if (_values.Any(v => v.PropertyId == propertyId && !isMultiple))
                throw new DomainException("Can't upload multiple data in this property");
            
            var value = ContactValue.Create(
                Id, 
                propertyId, 
                isSensitive
                    ? EncryptationExtension.Encrypt(data)
                    : data,
                TenantId);
            _values.Add(value);
        }

        public void UpdateValue(string data, Guid propertyId, bool isMultiple = false)
        {
            var exists = _values.FirstOrDefault(v => v.PropertyId == propertyId && !isMultiple);
            if (exists is not null)
                exists.Data = data;
            else
            {
                exists = ContactValue.Create(Id, propertyId, data, TenantId);
                _values.Add(exists);
                
            }
        }

        public void RemoveValue(Guid propertyId)
        {
            var founded = _values.FirstOrDefault(v => v.PropertyId == propertyId)
                ?? throw new DomainException("Value not found");
            
            _values.Remove(founded);
        }

        private void ChangeIdentity(Identity identity) => Identity = identity;
        private void ChangeAssociatedCompany(Guid associatedCompanyId) => AssociatedCompanyId = associatedCompanyId;
        private void ChangeInformation(string cin, string phone) => (Cin, Phone) = (cin, phone);
    }
}