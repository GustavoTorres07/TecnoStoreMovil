namespace TecnoStoreMovil.Shared.DTOs
{
    public record CarritoItemDto(
        int ProductoId,
        string Producto,
        string? ImagenUrl,
        decimal PrecioUnit,
        int Cantidad,
        int StockDisponible,
        decimal Subtotal
    );

    public record CarritoDto(
        int Id,
        int UsuarioId,
        List<CarritoItemDto> Items,
        decimal Total
    );

    public record CarritoAddItemRequest(int UsuarioId, int ProductoId, int Cantidad);
    public record CarritoUpdateQtyRequest(int UsuarioId, int ProductoId, int Cantidad);
}
