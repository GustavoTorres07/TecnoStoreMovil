// ICategoriaService.cs (solo lectura)
using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Api.Services.Contrato;

public interface ICategoriaService
{
    Task<IReadOnlyList<CategoriaDto>> GetAllAsync(CancellationToken ct);
    Task<CategoriaDto?> GetByIdAsync(int id, CancellationToken ct);
}
