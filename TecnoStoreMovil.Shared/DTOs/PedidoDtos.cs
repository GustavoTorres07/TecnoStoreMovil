namespace TecnoStoreMovil.Shared.DTOs
{
   
    public record PedidoResumenDto(
        int Id,
        DateTime Fecha,
        string Estado,
        decimal Total,
        string? Cliente = null, 
        int? UsuarioId = null   
    );

    public record PedidoItemLineaDto(
        int ProductoId,
        string Producto,
        decimal PrecioUnit,
        int Cantidad,
        decimal Subtotal
    );

    public record PedidoDto(
        int Id,
        DateTime Fecha,
        string Estado,
        decimal Total,
        List<PedidoItemLineaDto> Items,
        string? Cliente = null,
        int? UsuarioId = null
    );

    public record PedidoCrearDesdeCarritoRequest(int UsuarioId);

    public record PedidoCambiarEstadoRequest(string Estado, string? Motivo);
}
