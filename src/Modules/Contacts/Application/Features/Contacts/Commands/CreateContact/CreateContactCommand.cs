using System.Text.Json.Serialization;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Mcm.Contacts.Application.Features.Contacts.Commands.CreateContact
{
    public class CreateContactCommand
        : IRequest<ApiResponse<CreateContactResponse>>
    {
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string Poste { get; set; } = null!;
        public string? Email { get; set; }
        public string? Cin { get; set; }
        public string? Phone { get; set; }
        public IFormFile? Image { get; set; }
        public Guid AssociatedCompanyId { get; set; }
        public List<ContactValueAdd> SupplementaryValues { get; set; } = [];
    }
}