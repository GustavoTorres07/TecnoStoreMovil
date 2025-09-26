using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Api.Services.Contrato
{
    public interface IAdminPedidosService
    {
        Task<List<PedidoResumenDto>> ListAsync(string? estado, CancellationToken ct = default);
        Task<PedidoDto?> GetAsync(int id, CancellationToken ct = default);
        Task<bool> AceptarAsync(int id, CancellationToken ct = default);
        Task<bool> RechazarAsync(int id, string? motivo, CancellationToken ct = default);
    }
}
