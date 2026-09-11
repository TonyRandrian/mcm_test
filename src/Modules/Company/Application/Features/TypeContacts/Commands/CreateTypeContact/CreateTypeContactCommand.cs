using System.Runtime.Serialization;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Mcm.Company.Application.Features.TypeContacts.Commands.CreateTypeContact
{
    public class CreateTypeContactCommand
        : IRequest<ApiResponse<CreateTypeContactResponse>>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Color { get; set; }
        public Guid? TypeConvertTo { get; set; }
    }
}