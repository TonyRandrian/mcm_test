using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Property.Application.Features.Properties.Queries.GetProperty
{
    public record GetPropertyQuery(GetPropertyRequest Request)
        : IRequest<ApiResponse<GetPropertyResponse>>;
}