using Microsoft.Extensions.Logging;
using TecnoStoreMovil.Services.Contrato;
using TecnoStoreMovil.Services.Implementacion;

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

            // 🔹 Imprescindible para Blazor en MAUI
            builder.Services.AddMauiBlazorWebView();

            // 🔹 HttpClient + servicios del front
            builder.Services.AddHttpClient("api", c =>
            {
                // Usa tu URL pública de Somee (con / al final si tu API mapea raiz "/")
                c.BaseAddress = new Uri(TecnoStoreMovil.Config.ApiConfig.BaseUrl);
            });

            builder.Services.AddSingleton<ISesionService, SesionService>();
            builder.Services.AddTransient<IApiClient, ApiClient>();
            builder.Services.AddTransient<IAuthClient, AuthClient>();
            builder.Services.AddTransient<ICatalogoClient, CatalogoClient>();
            builder.Services.AddTransient<IUsuariosClient, UsuariosClient>();
            builder.Services.AddTransient<IAdminCategoriasClient, AdminCategoriasClient>();
            builder.Services.AddTransient<IAdminProductosClient, AdminProductosClient>();
            builder.Services.AddTransient<ICategoriasClient, CategoriasClient>();


#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}