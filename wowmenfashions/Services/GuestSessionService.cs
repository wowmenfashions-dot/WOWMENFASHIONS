using System;
using Microsoft.AspNetCore.Http;

namespace wowmenfashions.Services;

public class GuestSessionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string GuestCartCookieName = "GuestCartId";

    public GuestSessionService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetOrCreateGuestCartId()
    {
        var context = _httpContextAccessor.HttpContext;
        if (context == null) return Guid.NewGuid(); // Fallback for components out of HTTP context

        if (context.Request.Cookies.TryGetValue(GuestCartCookieName, out var cookieValue) && 
            Guid.TryParse(cookieValue, out var guestCartId))
        {
            return guestCartId;
        }

        guestCartId = Guid.NewGuid();
        context.Response.Cookies.Append(GuestCartCookieName, guestCartId.ToString(), new CookieOptions
        {
            HttpOnly = true,
            IsEssential = true,
            SameSite = SameSiteMode.Lax,
            Expires = DateTimeOffset.UtcNow.AddDays(30)
        });

        return guestCartId;
    }
}
