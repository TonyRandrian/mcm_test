using Mcm.Shared.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Mcm.Company.Application.Features.Company.Commands.CreateCompany
{
    public class CreateCompanyCommand : IRequest<ApiResponse<CreateCompanyResponse>>
    {
        public string Name { get; set; } = string.Empty;
        public string Acronym { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IFormFile? Logo { get; set; } = null;
        public bool IsContact { get; set; } = false;
        public Guid? ParentId { get; set; }
        public Guid? LeaderId { get; set; }
        public Guid? TypeContactId { get; set; }
        public List<Guid>? ActivitySectors { get; set; } = null;
        public List<CreateCompanyValue>? Values { get; set; } = [];
    }
}