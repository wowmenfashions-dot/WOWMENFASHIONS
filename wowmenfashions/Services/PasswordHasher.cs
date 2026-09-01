using Microsoft.AspNetCore.Identity;

namespace wowmenfashions.Services;

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hash);
}

public class PasswordHasher : IPasswordHasher
{
    private readonly PasswordHasher<string> _hasher = new();

    public string HashPassword(string password)
    {
        return _hasher.HashPassword("dummyUser", password);
    }

    public bool VerifyPassword(string password, string hash)
    {
        var result = _hasher.VerifyHashedPassword("dummyUser", hash, password);
        return result != PasswordVerificationResult.Failed;
    }
}
