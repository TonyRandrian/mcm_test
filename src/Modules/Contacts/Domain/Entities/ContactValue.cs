using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Domain.Primitives;

namespace Mcm.Contacts.Domain.Entities
{
    public class ContactValue
    {
        public Guid Id { get; private set; }
        public string Data { get; set; } = string.Empty;
        public Guid PropertyId { get; set; }
        public Guid ContactId { get; set; }
        public Guid TenantId { get; set; }

        private ContactValue() {}
        private ContactValue(Guid contactId, Guid propertyId, string data, Guid tenantId)
        {
            ContactId = contactId;
            PropertyId = propertyId;
            Data = data;
            TenantId = tenantId;
        }
        public static ContactValue Create(Guid contactId, Guid propertyId, string data, Guid tenantId)
            => new(contactId, propertyId, data, tenantId);

    }
}