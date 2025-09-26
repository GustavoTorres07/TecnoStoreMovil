using System.Net.Http.Json;
using TecnoStoreMovil.Services.Contrato;
using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Services.Implementa;

public class AdminCategoriasClient : IAdminCategoriasClient
{
    private readonly IApiClient _api;
    public AdminCategoriasClient(IApiClient api) => _api = api;

    public async Task<int?> CreateAsync(CategoriaSaveDto dto)
    {
        var http = await _api.CreateAsync();
        var resp = await http.PostAsJsonAsync("categorias", dto);
        if (!resp.IsSuccessStatusCode) return null;
        return await resp.Content.ReadFromJsonAsync<int>();
    }

    public async Task<bool> UpdateAsync(int id, CategoriaSaveDto dto)
    {
        var http = await _api.CreateAsync();
        var resp = await http.PutAsJsonAsync($"categorias/{id}", dto);
        return resp.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var http = await _api.CreateAsync();
        var resp = await http.DeleteAsync($"categorias/{id}");
        return resp.IsSuccessStatusCode;
    }
}
