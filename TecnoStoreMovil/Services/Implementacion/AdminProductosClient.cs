using System.Net.Http.Json;
using TecnoStoreMovil.Services.Contrato;
using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Services.Implementacion;

public class AdminProductosClient : IAdminProductosClient
{
    private readonly IApiClient _api;
    public AdminProductosClient(IApiClient api) => _api = api;

    public async Task<int?> CreateAsync(ProductoSaveDto dto)
    {
        var http = await _api.CreateAsync();
        var resp = await http.PostAsJsonAsync("productos", dto);
        if (!resp.IsSuccessStatusCode) return null;
        return await resp.Content.ReadFromJsonAsync<int>();
    }

    public async Task<bool> UpdateAsync(int id, ProductoSaveDto dto)
    {
        var http = await _api.CreateAsync();
        var resp = await http.PutAsJsonAsync($"productos/{id}", dto);
        return resp.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var http = await _api.CreateAsync();
        var resp = await http.DeleteAsync($"productos/{id}");
        return resp.IsSuccessStatusCode;
    }
}
