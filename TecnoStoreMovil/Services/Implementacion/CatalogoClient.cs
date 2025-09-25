using System.Net.Http.Json;
using TecnoStoreMovil.Services.Contrato;
using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Services.Implementacion;

public class CatalogoClient : ICatalogoClient
{
    private readonly IApiClient _api;
    public CatalogoClient(IApiClient api) => _api = api;

    public async Task<List<CategoriaDto>> GetCategoriasAsync()
    {
        var http = await _api.CreateAsync();
        return await http.GetFromJsonAsync<List<CategoriaDto>>("categorias") ?? new();
    }

    public async Task<List<ProductoDto>> GetProductosAsync(int? categoriaId = null, string? q = null)
    {
        var http = await _api.CreateAsync();
        var url = "productos";
        var qs = new List<string>();
        if (categoriaId is > 0) qs.Add($"categoriaId={categoriaId}");
        if (!string.IsNullOrWhiteSpace(q)) qs.Add($"q={Uri.EscapeDataString(q)}");
        if (qs.Count > 0) url += "?" + string.Join("&", qs);
        return await http.GetFromJsonAsync<List<ProductoDto>>(url) ?? new();
    }

    public async Task<ProductoDto?> GetProductoAsync(int id)
    {
        var http = await _api.CreateAsync();
        return await http.GetFromJsonAsync<ProductoDto>($"productos/{id}");
    }
}
