using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Api.Services.Contrato
{
    public interface IProductoService
    {
        Task<IReadOnlyList<ProductoDto>> SearchAsync(int? categoriaId, string? q, CancellationToken ct);
        Task<ProductoDto?> GetByIdAsync(int id, CancellationToken ct);
    }
}
