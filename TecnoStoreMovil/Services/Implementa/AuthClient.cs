using System.Net.Http.Json;
using TecnoStoreMovil.Services.Contrato;
using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Services.Implementa;

public class AuthClient : IAuthClient
{
    private readonly IApiClient _api;
    private readonly ISesionService _sesion;

    public AuthClient(IApiClient api, ISesionService sesion)
    {
        _api = api;
        _sesion = sesion;
    }

    public async Task<bool> LoginAsync(string email, string clave)
    {
        try
        {
            var http = await _api.CreateAsync();

            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));

            var r = await http.PostAsJsonAsync("auth/login", new LoginRequest(email, clave), cts.Token);

            if (!r.IsSuccessStatusCode)
                return false; 

            var dto = await r.Content.ReadFromJsonAsync<LoginResponse>(cancellationToken: cts.Token);
            if (dto is null) return false;

            await _sesion.SetAsync(dto.SessionId, dto.UsuarioId, dto.Rol, dto.Nombre/*, dto.Apellido*/);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public void Logout() => _sesion.Logout();
}
