namespace Mcm.Shared.Application.Common
{
    public record ResourceResponse
    (
        string FileType,
        string Url,
        string? AlternativeText,
        string StorageType

    );
}