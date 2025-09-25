using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Api.Services.Contrato;

public interface IProductoAdminService
{
    Task<int> CreateAsync(ProductoSaveDto dto, CancellationToken ct);
    Task UpdateAsync(int id, ProductoSaveDto dto, CancellationToken ct);
    Task DeleteAsync(int id, CancellationToken ct);
}
