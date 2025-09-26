namespace TecnoStoreMovil.Shared.Models
{
    public class PedidoItem
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }         
        public decimal PrecioUnit { get; set; }    
        public decimal Subtotal => Cantidad * PrecioUnit;
        public Pedido Pedido { get; set; } = null!;
        public Producto Producto { get; set; } = null!;
    }
}
