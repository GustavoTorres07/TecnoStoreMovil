using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Services.Contrato
{
    public interface ICarritoClient
    {
        Task<CarritoDto?> GetAsync();
        Task<bool> AddItemAsync(int productoId, int cantidad);
        Task<bool> UpdateQtyAsync(int productoId, int cantidad);
        Task<bool> RemoveItemAsync(int productoId);
        Task<bool> ClearAsync();
    }
}
