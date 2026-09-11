namespace Mcm.Shared.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException() : base("Bad Request for entity")
        {
        }

        public BadRequestException(string message) : base(message)
        {
        }

        public static BadRequestException Exist(string entity) => new($"Bad Request: '{entity}' already exists");
    }
}