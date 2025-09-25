using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Services.Contrato;

public interface IAdminCategoriasClient
{
    Task<int?> CreateAsync(CategoriaSaveDto dto);
    Task<bool> UpdateAsync(int id, CategoriaSaveDto dto);
    Task<bool> DeleteAsync(int id);
}
