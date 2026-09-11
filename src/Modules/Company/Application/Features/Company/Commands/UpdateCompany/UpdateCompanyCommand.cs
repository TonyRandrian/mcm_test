using System.Text.Json.Serialization;
using Mcm.Shared.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Mcm.Company.Application.Features.Company.Commands.UpdateCompany
{
    public class UpdateCompanyCommand
        : IRequest<ApiResponse<UpdateCompanyResponse>>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Acronym { get; set; }
        public string? Description { get; set; }
        public IFormFile? Logo { get; set; }
        public List<Guid>? ActivitySectors { get; set; } = [];
    }
}