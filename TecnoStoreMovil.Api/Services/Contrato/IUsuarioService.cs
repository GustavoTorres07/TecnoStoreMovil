using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Api.Services.Contrato
{
    public interface IUsuarioService
    {
        Task<IReadOnlyList<UsuarioDto>> SearchAsync(string? q, CancellationToken ct);
        Task<UsuarioDto?> GetByIdAsync(int id, CancellationToken ct);
        Task<int> CreateAsync(UsuarioSaveDto dto, CancellationToken ct);
        Task UpdateAsync(int id, UsuarioSaveDto dto, CancellationToken ct);
        Task DeleteAsync(int id, CancellationToken ct);
    }
}
