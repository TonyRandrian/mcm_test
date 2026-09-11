using Microsoft.AspNetCore.Http;

namespace Mcm.Authorizations.Application.Features.TeamMembers.Commands.UploadPhoto
{
    public record UploadPhotoRequest
    (
        IFormFile Image
    );
}