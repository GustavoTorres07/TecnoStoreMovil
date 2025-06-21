using Microsoft.Extensions.Logging;
using TecnoStoreMovil.Services;

namespace TecnoStoreMovil
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

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddSingleton<UsuarioService>();
            builder.Services.AddSingleton<SesionService>();
            builder.Services.AddSingleton<ProductoService>();
            builder.Services.AddSingleton<CarritoService>();
            builder.Services.AddSingleton<CategoriaService>();
            builder.Services.AddSingleton<PedidoService>();






#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
