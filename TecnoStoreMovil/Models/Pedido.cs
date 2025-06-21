using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecnoStoreMovil.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public string ClienteNombre { get; set; } = "";
        public DateTime Fecha { get; set; }
        public EstadoPedido Estado { get; set; }
        public string ResumenProductos { get; set; } = "";
        // Otros campos que consideres necesarios
    }
}
