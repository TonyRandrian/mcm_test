using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Domain.Exceptions;
using Mcm.Shared.Domain.Primitives;

namespace Mcm.Shared.Domain.ValueObjects
{
    public class Resource : ValueObject
    {
        public StorageType StorageType { get; set; }
        public string Url { get; set; } = string.Empty;
        public string AlternativeText { get; set; } = string.Empty;
        public FileType FileType { get; set; }
        public string? ContentType { get; set; }
        
        public static Resource Document(StorageType storageType, string url, string alternativeText, string? contentType = "image/")
        {
            return new Resource
            {
                StorageType = storageType,
                Url = url,
                AlternativeText = alternativeText,
                FileType = FileType.Document,
                ContentType = contentType
            };
        }

        public static Resource Image(StorageType storageType, string url, string alternativeText, string? contenyType = "text/")
        {
            return new Resource
            {
                StorageType = storageType,
                Url = url,
                AlternativeText = alternativeText,
                FileType = FileType.Image,
                ContentType = contenyType
            };
        }

        public override IEnumerable<string> GetAtomicValues()
        {
            yield return AlternativeText;
            yield return FileType.ToString();
            yield return StorageType.ToString();
            yield return Url;
            yield return ContentType ?? string.Empty;
        }

    }
}