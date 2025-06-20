using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecnoStoreMovil.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Apellido es obligatorio.")]
        public string Apellido { get; set; } 

        [Required(ErrorMessage = "El DNI es obligatorio.")]
        public string Dni { get; set; }

        [Required(ErrorMessage = "El Email es obligatorio.")]
        public string Email { get; set; } 
        public string Celular { get; set; }

        [Required(ErrorMessage = "La Clave es obligatorio.")]
        public string Clave { get; set; } 
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public string FotoUsuario { get; set; } 
        public bool Activo { get; set; }

        [Required(ErrorMessage = "Debe Seleccionar un Rol para el Usuario.")]
        public RolUsuario Rol { get; set; }

    }
}
