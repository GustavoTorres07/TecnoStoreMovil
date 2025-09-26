using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Services.Contrato
{
    public interface IAdminPedidosClient
    {
        Task<List<PedidoResumenDto>> ListAsync(string? estado);
        Task<PedidoDto?> GetAsync(int id);
        Task<bool> AceptarAsync(int id);
        Task<bool> RechazarAsync(int id, string? motivo);
    }
}
