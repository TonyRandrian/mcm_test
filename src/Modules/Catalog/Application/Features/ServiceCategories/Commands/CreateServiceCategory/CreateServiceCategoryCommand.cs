using System.Runtime.Serialization;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Mcm.Catalog.Application.Features.ServiceCategories.Commands.CreateServiceCategory
{
    public class CreateServiceCategoryCommand
        : IRequest<ApiResponse<CreateServiceCategoryResponse>>
    {
        public Guid? ParentCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}