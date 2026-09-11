using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Authorizations.Application.Features.TeamMembers.Commands.UploadPhoto
{
    public class UploadPhotoResponse
    {
        public Resource Image { get; set; } = new();
    }
}