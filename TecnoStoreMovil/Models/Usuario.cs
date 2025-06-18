using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecnoStoreMovil.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } 
        public string Apellido { get; set; } 
        public string Dni { get; set; } 
        public string Email { get; set; } 
        public string Celular { get; set; }
        public string Clave { get; set; } 
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public string FotoUsuario { get; set; } 
        public bool Activo { get; set; }
        public RolUsuario Rol { get; set; }

    }
}
