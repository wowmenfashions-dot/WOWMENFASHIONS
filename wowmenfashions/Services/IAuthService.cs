namespace wowmenfashions.Services;

public interface IAuthService
{
    Task<bool> ValidateAdminAsync(string username, string password);
}
