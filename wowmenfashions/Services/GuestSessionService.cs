using System;
using Microsoft.AspNetCore.Http;

namespace wowmenfashions.Services;

public class GuestSessionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string GuestCartCookieName = "GuestCartId";

    private Guid? _cachedCartId;

    public GuestSessionService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetOrCreateGuestCartId()
    {
        if (_cachedCartId.HasValue) return _cachedCartId.Value;

        var context = _httpContextAccessor.HttpContext;
        if (context == null)
        {
            _cachedCartId = Guid.NewGuid();
            return _cachedCartId.Value;
        }

        if (context.Request.Cookies.TryGetValue(GuestCartCookieName, out var cookieValue) && 
            Guid.TryParse(cookieValue, out var guestCartId))
        {
            _cachedCartId = guestCartId;
            return guestCartId;
        }

        _cachedCartId = Guid.NewGuid();
        
        try 
        {
            context.Response.Cookies.Append(GuestCartCookieName, _cachedCartId.Value.ToString(), new CookieOptions
            {
                HttpOnly = true,
                IsEssential = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddDays(30)
            });
        }
        catch { } // Ignore exception if response has already started in Blazor InteractiveServer

        return _cachedCartId.Value;
    }
}
