using System.Text.Json.Serialization;
using Mcm.Shared.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Mcm.Company.Application.Features.Company.Commands.DeleteCompany
{
    public class DeleteCompanyCommand
        : IRequest<ApiResponse<DeleteCompanyResponse>>
    {
        public Guid Id { get; set; }
        public bool Force { get; set; }
    }
}