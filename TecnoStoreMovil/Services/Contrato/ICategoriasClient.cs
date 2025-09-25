// ICategoriasClient.cs
using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Services.Contrato;

public interface ICategoriasClient
{
    Task<List<CategoriaDto>> GetAllAsync();
    Task<CategoriaDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(CategoriaSaveDto dto);
    Task UpdateAsync(int id, CategoriaSaveDto dto);
    Task DeleteAsync(int id);
}
