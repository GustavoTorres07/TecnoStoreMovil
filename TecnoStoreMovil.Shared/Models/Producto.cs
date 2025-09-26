namespace TecnoStoreMovil.Shared.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public int CategoriaId { get; set; }
        public string Nombre { get; set; } = string.Empty; 
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string? ImagenUrl { get; set; }
        public bool Activo { get; set; }
        public Categoria Categoria { get; set; } = null!;
        public ICollection<PedidoItem> PedidoItems { get; set; } = new List<PedidoItem>();
        public ICollection<CarritoItem> CarritoItems { get; set; } = new List<CarritoItem>();

    }
}
