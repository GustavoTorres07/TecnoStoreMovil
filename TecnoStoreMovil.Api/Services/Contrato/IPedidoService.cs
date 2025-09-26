using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Api.Services.Contrato
{
    public interface IPedidosService
    {
        Task<int?> CrearDesdeCarritoAsync(int usuarioId, CancellationToken ct = default);
        Task<List<PedidoResumenDto>> MisPedidosAsync(int usuarioId, string? estado, CancellationToken ct = default);
        Task<PedidoDto?> ObtenerDetalleAsync(int pedidoId, CancellationToken ct = default);
    }
}
