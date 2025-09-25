using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecnoStoreMovil.Shared.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Estado { get; set; } = EstadoPedido.Pendiente; // default
        public decimal Total { get; set; }
        public Usuario Usuario { get; set; } = null!;
        public ICollection<PedidoItem> Items { get; set; } = new List<PedidoItem>();
    }
}
