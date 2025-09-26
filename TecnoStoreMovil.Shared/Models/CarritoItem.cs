namespace TecnoStoreMovil.Shared.Models;

public class CarritoItem
{
    public int Id { get; set; }
    public int CarritoId { get; set; }
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnit { get; set; }

    public Carrito? Carrito { get; set; }
    public Producto? Producto { get; set; }
    public decimal Subtotal => Cantidad * PrecioUnit;

}
