using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Services.Contrato;

public interface ICatalogoClient
{
    Task<List<CategoriaDto>> GetCategoriasAsync();
    Task<List<ProductoDto>> GetProductosAsync(int? categoriaId = null, string? q = null);
    Task<ProductoDto?> GetProductoAsync(int id);
}
