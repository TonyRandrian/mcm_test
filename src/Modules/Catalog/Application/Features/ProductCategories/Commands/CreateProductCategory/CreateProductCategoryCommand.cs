using System.Runtime.Serialization;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Mcm.Catalog.Application.Features.ProductCategories.Commands.CreateProductCategory
{
    public class CreateProductCategoryCommand
        : IRequest<ApiResponse<CreateProductCategoryResponse>>
    {
        public Guid? ParentCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}