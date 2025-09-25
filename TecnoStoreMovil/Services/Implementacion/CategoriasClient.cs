// CategoriasClient.cs
using System.Net.Http.Json;
using TecnoStoreMovil.Services.Contrato;
using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Services.Implementacion;

public class CategoriasClient : ICategoriasClient
{
    private readonly HttpClient _http;

    public CategoriasClient(HttpClient http) => _http = http;

    public async Task<List<CategoriaDto>> GetAllAsync()
        => await _http.GetFromJsonAsync<List<CategoriaDto>>("api/categorias") ?? new();

    public async Task<CategoriaDto?> GetByIdAsync(int id)
        => await _http.GetFromJsonAsync<CategoriaDto>($"api/categorias/{id}");

    public async Task<int> CreateAsync(CategoriaSaveDto dto)
    {
        var res = await _http.PostAsJsonAsync("api/categorias", dto);
        res.EnsureSuccessStatusCode();
        return await res.Content.ReadFromJsonAsync<int>();
    }

    public async Task UpdateAsync(int id, CategoriaSaveDto dto)
    {
        var res = await _http.PutAsJsonAsync($"api/categorias/{id}", dto);
        res.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(int id)
    {
        var res = await _http.DeleteAsync($"api/categorias/{id}");
        res.EnsureSuccessStatusCode();
    }
}
