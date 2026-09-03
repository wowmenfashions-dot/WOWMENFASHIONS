using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Hangfire;
using MudBlazor.Services;
using wowmenfashions.Components;
using wowmenfashions.Data;
using wowmenfashions.Repositories;
using wowmenfashions.Services;
using wowmenfashions.Models;
using wowmenfashions.Services;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddMemoryCache();

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("AdminLoginLimiter", opt =>
    {
        opt.PermitLimit = 5;
        opt.Window = TimeSpan.FromMinutes(15);
    });
});

// Register SQL Connection Factory for Dapper
builder.Services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<GuestSessionService>();

builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IHomepageConfigRepository, HomepageConfigRepository>();
builder.Services.AddScoped<IHomepageConfigService, HomepageConfigService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IStoreSettingsRepository, StoreSettingsRepository>();
builder.Services.AddScoped<IStoreSettingsService, StoreSettingsService>();

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IErrorLoggerService, ErrorLoggerService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICheckoutService, CheckoutService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IContactMessageService, ContactMessageService>();
builder.Services.AddScoped<IPageContentService, PageContentService>();
builder.Services.AddScoped<IRazorpayService, RazorpayService>();
builder.Services.AddScoped<IAdminAuditService, AdminAuditService>();
builder.Services.AddScoped<IEncryptionService, EncryptionService>();

builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHangfireServer();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "auth_token";
        options.LoginPath = "/login";
        options.Cookie.MaxAge = TimeSpan.FromMinutes(60);
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/access-denied";
        options.Events = new CookieAuthenticationEvents
        {
            OnRedirectToLogin = context =>
            {
                if (context.Request.Path.StartsWithSegments("/admin"))
                {
                    context.Response.Redirect("/admin/login" + context.Request.QueryString);
                }
                else
                {
                    context.Response.Redirect(context.RedirectUri);
                }
                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthorization();
builder.Services.AddControllers();

var cultureInfo = new System.Globalization.CultureInfo("en-IN");
System.Globalization.CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var app = builder.Build();

var supportedCultures = new[] { "en-IN" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

app.Use(async (context, next) =>
{
    context.Response.Headers.Append("Content-Security-Policy", 
        "default-src 'self'; " +
        "script-src 'self' 'unsafe-inline' https://checkout.razorpay.com https://cdn.razorpay.com https://cdn.quilljs.com https://www.googletagmanager.com https://www.google-analytics.com; " +
        "style-src 'self' 'unsafe-inline' https://fonts.googleapis.com https://cdn.quilljs.com; " +
        "font-src 'self' https://fonts.gstatic.com; " +
        "img-src 'self' data: https:; " +
        "connect-src 'self' wss: https:; " +
        "frame-src 'self' https://api.razorpay.com;");
    await next();
});

app.UseHttpsRedirection();
app.UseRouting();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapControllers();
app.UseHangfireDashboard("/admin/jobs"); // Secured by default for local, needs auth filter for prod

RecurringJob.AddOrUpdate<CartAbandonmentJob>("cart-abandonment", job => job.ProcessAbandonedCartsAsync(), Cron.Hourly);

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
