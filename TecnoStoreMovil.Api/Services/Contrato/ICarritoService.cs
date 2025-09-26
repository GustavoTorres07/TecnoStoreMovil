using TecnoStoreMovil.Shared.DTOs;
using TecnoStoreMovil.Shared.Models;

namespace TecnoStoreMovil.Api.Services.Contrato
{
    public interface ICarritoService
    {
        Task<Carrito?> ObtenerCarritoAbiertoAsync(int usuarioId, CancellationToken ct = default);
        Task<Carrito> ObtenerOCrearCarritoAbiertoAsync(int usuarioId, CancellationToken ct = default);

        Task<CarritoDto?> ObtenerDtoAsync(int usuarioId, CancellationToken ct = default);
        Task<bool> AgregarItemAsync(int usuarioId, int productoId, int cantidad, CancellationToken ct = default);
        Task<bool> ActualizarCantidadAsync(int usuarioId, int productoId, int cantidad, CancellationToken ct = default);
        Task<bool> EliminarItemAsync(int usuarioId, int productoId, CancellationToken ct = default);
        Task<bool> VaciarAsync(int usuarioId, CancellationToken ct = default);
    }
}
