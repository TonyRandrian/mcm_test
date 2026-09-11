using System.Text.Json.Serialization;
using Mcm.Shared.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Mcm.Contacts.Application.Features.Contacts.Commands.DeleteContact
{
    public class DeleteContactCommand
        : IRequest<ApiResponse<DeleteContactResponse>>
    {
       public Guid Id { get; set; }
       public bool Force { get; set; }
    }
}