namespace Mcm.Authorizations.Application.Interfaces
{
    public interface IHashPasswordService
    {
        string HashPassword(string password);
        bool VerifyPassword(string hashedPassword, string providedPassword);
    }
}