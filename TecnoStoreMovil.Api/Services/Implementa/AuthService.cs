using Microsoft.EntityFrameworkCore;
using TecnoStoreMovil.Api.Data;
using TecnoStoreMovil.Api.Services.Contrato;

namespace TecnoStoreMovil.Api.Services.Implementa;

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;
    public AuthService(AppDbContext db) => _db = db;

    public async Task<(bool Ok, int UsuarioId, string Nombre, string Rol)> LoginAsync(string email, string clave, CancellationToken ct)
    {
        var user = await _db.Usuarios
            .AsNoTracking()
            .Where(u => u.Email == email && u.Clave == clave && u.Activo)
            .Select(u => new { u.Id, u.Nombre })
            .FirstOrDefaultAsync(ct);

        if (user is null) return (false, 0, "", "");

        var rol = await _db.UsuarioRoles
            .AsNoTracking()
            .Where(ur => ur.UsuarioId == user.Id)
            .Join(_db.Roles, ur => ur.RolId, r => r.Id, (ur, r) => r.Nombre)
            .FirstOrDefaultAsync(ct) ?? "Cliente";

        return (true, user.Id, user.Nombre, rol);
    }
}
