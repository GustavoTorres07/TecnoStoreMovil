namespace TecnoStoreMovil.Shared.Models;

public class Carrito
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public string Estado { get; set; } = EstadoCarrito.Abierto;
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    public Usuario? Usuario { get; set; }
    public ICollection<CarritoItem> Items { get; set; } = new List<CarritoItem>();

}
