using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecnoStoreMovil.Shared.Models
{
    public class Direccion
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }        // UNIQUE en BD asegura 1 por usuario
        public string Calle { get; set; } = string.Empty;
        public string? Numero { get; set; }
        public string Ciudad { get; set; } = string.Empty;
        public string Provincia { get; set; } = string.Empty;
        public string CodigoPostal { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;

        // Nav
        public Usuario Usuario { get; set; } = null!;
    }
}
