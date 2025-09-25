using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Services.Contrato;

public interface IAdminProductosClient
{
    Task<int?> CreateAsync(ProductoSaveDto dto);
    Task<bool> UpdateAsync(int id, ProductoSaveDto dto);
    Task<bool> DeleteAsync(int id);
}
