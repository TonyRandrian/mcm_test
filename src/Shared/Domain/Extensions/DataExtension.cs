namespace Mcm.Shared.Domain.Extensions
{
    public static class DataExtension
    {
        extension(string data)
        {
            public string SetSensitive()
            {
                return EncryptationExtension.Encrypt(data);
            }
        }
    }
}