using System.Net.Http.Headers;
using TecnoStoreMovil.Services.Contrato;

namespace TecnoStoreMovil.Services.Implementacion;

public class ApiClient : IApiClient
{
    private readonly IHttpClientFactory _factory;
    private readonly ISesionService _sesion;

    public ApiClient(IHttpClientFactory factory, ISesionService sesion)
    {
        _factory = factory;
        _sesion = sesion;
    }

    public async Task<HttpClient> CreateAsync()
    {
        var c = _factory.CreateClient("api");
        c.DefaultRequestHeaders.Accept.Clear();
        c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var sid = await _sesion.GetSessionIdAsync();
        if (!string.IsNullOrWhiteSpace(sid))
        {
            if (c.DefaultRequestHeaders.Contains("X-Session-Id"))
                c.DefaultRequestHeaders.Remove("X-Session-Id");
            c.DefaultRequestHeaders.Add("X-Session-Id", sid);
        }
        return c;
    }
}
