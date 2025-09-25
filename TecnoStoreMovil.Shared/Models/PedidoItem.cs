using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecnoStoreMovil.Shared.Models
{
    public class PedidoItem
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }          // > 0 (check en BD)
        public decimal PrecioUnit { get; set; }    // snapshot del momento de compra

        // NOTA: en la BD hay una columna computada (cantidad*precio_unit).
        // Si querés exponerla en memoria:
        public decimal Subtotal => Cantidad * PrecioUnit;

        // Nav
        public Pedido Pedido { get; set; } = null!;
        public Producto Producto { get; set; } = null!;
    }
}
