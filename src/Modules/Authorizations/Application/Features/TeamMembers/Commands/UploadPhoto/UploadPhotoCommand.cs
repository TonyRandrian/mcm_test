using Mcm.Shared.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Mcm.Authorizations.Application.Features.TeamMembers.Commands.UploadPhoto
{
    public class UploadPhotoCommand
        : IRequest<ApiResponse<UploadPhotoResponse>>
    {
        public Guid Id { get; set; }
        public IFormFile Image { get; set; } = null!;
    }
}