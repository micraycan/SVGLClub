using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using SVGLClub.Components;
using SVGLClub.Data;
using SVGLClub.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// http
builder.Services.AddHttpClient();

// MudBlazor
builder.Services.AddMudServices();

// Entity Framework
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// car config 
builder.Services.Configure<List<CarClassConfig>>(builder.Configuration.GetSection("CarClasses"));

// services
builder.Services.AddScoped<IAPISessionProvider, APISessionProvider>();
builder.Services.AddScoped<IContentLoader, ContentLoader>();
builder.Services.AddScoped<IDriverStatService, DriverStatService>();
builder.Services.AddScoped<IQueryService, QueryService>();
builder.Services.AddScoped<IRemoteFileService, RemoteFileService>();
builder.Services.AddScoped<IServerConfigLoader, ServerConfigLoader>();
builder.Services.AddScoped<ISessionDBSaver, SessionDBSaver>();
builder.Services.AddScoped<ISessionImportService, SessionImportService>();
builder.Services.AddScoped<ISessionJsonDeserializer, SessionJsonDeserializer>();
builder.Services.AddScoped<ISessionMapper, SessionMapper>();
builder.Services.AddScoped<ISessionStateService, SessionStateService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();


app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
