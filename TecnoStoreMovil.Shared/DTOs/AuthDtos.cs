using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecnoStoreMovil.Shared.DTOs
{
    public record LoginRequest(string Email, string Clave);

    // Si tu API devuelve SessionId (opcional):
    public record LoginResponse(string SessionId, int UsuarioId, string Rol, string Nombre);
}
