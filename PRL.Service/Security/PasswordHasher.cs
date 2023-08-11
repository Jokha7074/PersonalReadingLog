using BCrypt.Net;

namespace PRL.Service.Security;

public static class PasswordHasher
{
    public static (string PasswordHash, string Salt) Hash(string password)
    {
        string salt = Guid.NewGuid().ToString();
        string hash = BCrypt.Net.BCrypt.HashPassword(password + salt);
        return (hash, salt);
    }

    public static bool Verify(string password, string passwordHash, string salt)
    {
        return BCrypt.Net.BCrypt.Verify(password+salt, passwordHash);
    }
}
