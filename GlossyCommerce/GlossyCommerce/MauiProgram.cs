using GlossyCommerce.Services;
using GlossyCommerce.Shared.Services;
using Microsoft.Extensions.Logging;
using GlossyCommerce.Shared.Services;
namespace GlossyCommerce
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Add device-specific services used by the GlossyCommerce.Shared project
            builder.Services.AddSingleton<IFormFactor, FormFactor>();

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddSingleton<AuthService>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
