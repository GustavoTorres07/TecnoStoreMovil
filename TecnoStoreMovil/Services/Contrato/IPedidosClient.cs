using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Services.Contrato
{
    public interface IPedidosClient
    {
        Task<int?> CrearDesdeCarritoAsync(int usuarioId);
        Task<List<PedidoResumenDto>> MisPedidosAsync(int usuarioId, string? estado = null);
        Task<PedidoDto?> GetDetalleAsync(int pedidoId);
    }
}
