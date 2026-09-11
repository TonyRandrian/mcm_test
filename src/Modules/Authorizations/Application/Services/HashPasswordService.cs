using Mcm.Authorizations.Application.Interfaces;

namespace Mcm.Authorizations.Application.Services
{
    public class HashPasswordService : IHashPasswordService
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string hashedPassword, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}