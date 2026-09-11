namespace Mcm.Shared.Application.Exceptions
{
    public class UnauthorizedException(string message = "Access not authorized") : Exception(message)
    {
    }

}