using System.Net.Http.Json;
using TecnoStoreMovil.Services.Contrato;
using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Services.Implementacion;

public class UsuariosClient : IUsuariosClient
{
    private readonly IApiClient _api;
    public UsuariosClient(IApiClient api) => _api = api;

    public async Task<List<UsuarioDto>> SearchAsync(string? q = null)
    {
        var http = await _api.CreateAsync();
        var url = "usuarios";
        if (!string.IsNullOrWhiteSpace(q)) url += $"?q={Uri.EscapeDataString(q)}";
        return await http.GetFromJsonAsync<List<UsuarioDto>>(url) ?? new();
    }

    public async Task<UsuarioDto?> GetByIdAsync(int id)
    {
        var http = await _api.CreateAsync();
        return await http.GetFromJsonAsync<UsuarioDto>($"usuarios/{id}");
    }

    public async Task<int?> CreateAsync(UsuarioSaveDto dto)
    {
        var http = await _api.CreateAsync();
        var r = await http.PostAsJsonAsync("usuarios", dto);
        if (!r.IsSuccessStatusCode) return null;
        return await r.Content.ReadFromJsonAsync<int>();
    }

    public async Task<bool> UpdateAsync(int id, UsuarioSaveDto dto)
    {
        var http = await _api.CreateAsync();
        var r = await http.PutAsJsonAsync($"usuarios/{id}", dto);
        return r.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var http = await _api.CreateAsync();
        var r = await http.DeleteAsync($"usuarios/{id}");
        return r.IsSuccessStatusCode;
    }
}
