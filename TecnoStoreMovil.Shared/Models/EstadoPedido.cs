using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecnoStoreMovil.Shared.Models
{
    // Helpers de estado (opcional)
    public static class EstadoPedido
    {
        public const string Pendiente = "Pendiente";
        public const string Pagado = "Pagado";
        public const string Enviado = "Enviado";
        public const string Cancelado = "Cancelado";
    }
}
