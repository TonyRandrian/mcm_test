using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Mcm.Contacts.Application.Features.Contacts.Commands.UpdateContact
{
    public class UpdateContactCommand
        : IRequest<ApiResponse<UpdateContactResponse>>
    {
        public Guid Id { get; set; }
        public Guid? AssociatedCompanyId { get; set; }
        public string? LastName { get; set; }
        public string FirstName { get; set; } = null!;
        public string? Poste { get; set; }
        public string? Email { get; set; }
        public string? Cin { get; set; }
        public string? Phone { get; set; }
        public IFormFile? Image { get; set; }
    }
}