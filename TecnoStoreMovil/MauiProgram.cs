using Microsoft.Extensions.Logging;
using TecnoStoreMovil.Services.Contrato;
using TecnoStoreMovil.Services.Implementa;

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

            builder.Services.AddHttpClient("api", c =>
            {
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
            builder.Services.AddTransient<ICarritoClient, CarritoClient>();
            builder.Services.AddTransient<IPedidosClient, PedidosClient>();
            builder.Services.AddTransient<IAdminPedidosClient, AdminPedidosClient>();




#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}