// ICategoriaAdminService.cs (ABM)
using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Api.Services.Contrato;

public interface ICategoriaAdminService
{
    Task<int> CreateAsync(CategoriaSaveDto dto, CancellationToken ct);
    Task UpdateAsync(int id, CategoriaSaveDto dto, CancellationToken ct);
    Task DeleteAsync(int id, CancellationToken ct);
}
