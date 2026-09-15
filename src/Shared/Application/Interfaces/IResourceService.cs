using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Mcm.Shared.Application.Interfaces;

public interface IResourceService
{
    Task<Resource> SaveResource(IFormFile? file, FileType? fileType = FileType.Image);
    void DeleteResource(Resource image);
    (Stream Stream, string ContentType, string FileName) OpenResource(Resource resource);
}