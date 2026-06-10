using GlossyCommerce.Shared.Services;
using GlossyCommerce.Web.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GlossyCommerce.Shared.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the GlossyCommerce.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddScoped<AuthService>();
await builder.Build().RunAsync();
