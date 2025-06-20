using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecnoStoreMovil.Models;

namespace TecnoStoreMovil.Services
{
    public class SesionService
    {
        public Usuario? UsuarioActual { get; set; }

        public bool EstaLogueado => UsuarioActual != null;
        public bool EsAdministrador => UsuarioActual?.Rol == RolUsuario.Administrador;
        public bool EsCliente => UsuarioActual?.Rol == RolUsuario.Cliente;

        public void CerrarSesion()
        {
            UsuarioActual = null;
        }
    }
}
