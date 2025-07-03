using MudBlazor.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using AspNet.Security.OpenId.Steam;
using SVGLClub.Data;

using SVGLClub.Components;
using Microsoft.AspNetCore.Authentication;
using SVGLClub.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// http
builder.Services.AddHttpClient();

// Mudblazor
builder.Services.AddMudServices();

// EF Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthorization();

builder.Services.AddScoped<ISessionSyncService, SessionSyncService>();
builder.Services.AddScoped<IContentLoader, ContentLoader>();
builder.Services.AddScoped<IQueryService, QueryService>();
builder.Services.AddScoped<ITelemetryParser, TelemetryParser>();

// Steam
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = SteamAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie()
.AddSteam(options =>
{
    options.ApplicationKey = builder.Configuration["Steam:ApiKey"];
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseRouting();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapGet("/login", async (HttpContext http) =>
{
    var props = new AuthenticationProperties { RedirectUri = "/" };
    await http.ChallengeAsync(SteamAuthenticationDefaults.AuthenticationScheme, props);
});

app.MapGet("/logout", async (HttpContext http) =>
{
    await http.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    http.Response.Redirect("/");
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
