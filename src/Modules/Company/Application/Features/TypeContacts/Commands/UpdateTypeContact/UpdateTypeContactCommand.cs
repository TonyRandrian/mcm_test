using System.Text.Json.Serialization;
using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Company.Application.Features.TypeContacts.Commands.UpdateTypeContact
{
    public class UpdateTypeContactCommand
        : IRequest<ApiResponse<UpdateTypeContactResponse>>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Color { get; set; }
        public string? Description { get; set; }
    }
}