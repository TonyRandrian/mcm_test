using Mcm.Shared.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mcm.Contacts.Application.Features.Contacts.Commands.CreateContact
{
    public class CreateContactRequest
    {
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Poste { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Cin { get; set; }
        public string? Phone { get; set; }
        public IFormFile? Image { get; set; }
        public Guid AssociatedCompanyId { get; set; }

        public string? SupplementaryValues { get; set; } = null;
    };

    public class ContactValueAdd
    {
        public Guid PropertyId { get; set; }
        public string Value { get; set; } = string.Empty;
    }
}