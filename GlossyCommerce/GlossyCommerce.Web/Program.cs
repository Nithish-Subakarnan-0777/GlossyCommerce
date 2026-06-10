using GlossyCommerce.Shared.Services;
using GlossyCommerce.Web.Components;
using GlossyCommerce.Web.Services;
using GlossyCommerce.Data;
using Microsoft.EntityFrameworkCore;
using GlossyCommerce.Shared.Services;

var builder = WebApplication.CreateBuilder(args);
// Add Database Connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Add device-specific services used by the GlossyCommerce.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddControllers();
builder.Services.AddScoped<CartService>();
// Replace YOUR_PORT with the HTTPS port from your launchSettings.json
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7233/") });
builder.Services.AddScoped<AuthService>();
var app = builder.Build();
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(
        typeof(GlossyCommerce.Shared._Imports).Assembly,
        typeof(GlossyCommerce.Web.Client._Imports).Assembly);

app.Run();
