using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using wowmenfashions.Services;

namespace wowmenfashions.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("admin-login")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Login([FromForm] string username, [FromForm] string password, [FromQuery] string? returnUrl)
    {
        if (await _authService.ValidateAdminAsync(username, password))
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { IsPersistent = true };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
            
            return Redirect(string.IsNullOrEmpty(returnUrl) ? "/admin" : returnUrl);
        }

        return Redirect($"/admin/login?error=Invalid credentials&returnUrl={returnUrl}");
    }
    [HttpPost("login")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> CustomerLogin([FromForm] string email, [FromForm] string password, [FromQuery] string? returnUrl)
    {
        var customerService = HttpContext.RequestServices.GetRequiredService<ICustomerService>();
        var (success, customer, error) = await customerService.LoginAsync(email, password);

        if (success && customer != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, customer.Email),
                new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
                new Claim(ClaimTypes.GivenName, customer.FirstName),
                new Claim("FullName", $"{customer.FirstName} {customer.LastName}".Trim())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { IsPersistent = true };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
            
            return Redirect(string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl);
        }

        return Redirect($"/login?ErrorMessage={Uri.EscapeDataString(error)}&returnUrl={returnUrl}");
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/");
    }
}
