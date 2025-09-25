using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecnoStoreMovil.Shared.DTOs
{
    public record PedidoItemDto(
        int Id,
        int ProductoId,
        string ProductoNombre,
        int Cantidad,
        decimal PrecioUnit,
        decimal Subtotal
    );

    public record PedidoDto(
        int Id,
        int UsuarioId,
        string UsuarioNombre,
        DateTime FechaCreacion,
        string Estado,
        decimal Total,
        IReadOnlyList<PedidoItemDto> Items
    );

    // Para listados/gestión admin
    public record PedidoResumenDto(
        int Id,
        int UsuarioId,
        DateTime FechaCreacion,
        string Estado,
        decimal Total
    );

    // Para crear un pedido desde el front (ej.: “Finalizar compra”)
    public record PedidoCreateRequest(
        int UsuarioId,
        IReadOnlyList<PedidoCreateItem> Items
    );

    public record PedidoCreateItem(
        int ProductoId,
        int Cantidad
    );
}
