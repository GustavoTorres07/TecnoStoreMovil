using Microsoft.EntityFrameworkCore;
using TecnoStoreMovil.Api.Data;
using TecnoStoreMovil.Api.Services.Contrato;
using TecnoStoreMovil.Shared.DTOs;
using TecnoStoreMovil.Shared.Models;

namespace TecnoStoreMovil.Api.Services.Implementa;

public class UsuarioService : IUsuarioService
{
    private readonly AppDbContext _db;
    public UsuarioService(AppDbContext db) => _db = db;

    public async Task<IReadOnlyList<UsuarioDto>> SearchAsync(string? q, CancellationToken ct)
    {
        var users = _db.Usuarios.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.Trim();
            users = users.Where(u =>
                u.Email.Contains(term) ||
                u.Nombre.Contains(term) ||
                u.Apellido.Contains(term));
        }

        var data = await users
            .OrderBy(u => u.Nombre).ThenBy(u => u.Apellido)
            .Select(u => new UsuarioDto(
                u.Id,
                u.Nombre,
                u.Apellido,
                u.Email,
                u.Telefono,
                u.Activo,
                u.FechaAlta,
                u.Direccion == null
                    ? null
                    : new DireccionDto(
                        u.Direccion.Id,
                        u.Id,
                        u.Direccion.Calle,
                        u.Direccion.Numero,
                        u.Direccion.Ciudad,
                        u.Direccion.Provincia,
                        u.Direccion.CodigoPostal,
                        u.Direccion.Pais
                    ),
                _db.UsuarioRoles
                    .Where(ur => ur.UsuarioId == u.Id)
                    .Join(_db.Roles, ur => ur.RolId, r => r.Id, (ur, r) => r.Nombre)
                    .ToArray()
            ))
            .ToListAsync(ct);

        return data;
    }

    public async Task<UsuarioDto?> GetByIdAsync(int id, CancellationToken ct)
    {
        var u = await _db.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);
        if (u is null) return null;

        var roles = await _db.UsuarioRoles
            .AsNoTracking()
            .Where(ur => ur.UsuarioId == id)
            .Join(_db.Roles, ur => ur.RolId, r => r.Id, (ur, r) => r.Nombre)
            .ToArrayAsync(ct);

        DireccionDto? dir = null;
        if (u.Direccion is not null)
        {
            var d = u.Direccion;
            dir = new DireccionDto(d.Id, u.Id, d.Calle, d.Numero, d.Ciudad, d.Provincia, d.CodigoPostal, d.Pais);
        }

        return new UsuarioDto(
            u.Id, u.Nombre, u.Apellido, u.Email, u.Telefono, u.Activo, u.FechaAlta, dir, roles
        );
    }

    public async Task<int> CreateAsync(UsuarioSaveDto dto, CancellationToken ct)
    {
        var emailExists = await _db.Usuarios.AnyAsync(x => x.Email == dto.Email, ct);
        if (emailExists) throw new InvalidOperationException("El email ya está registrado.");

        var rol = await _db.Roles.FirstOrDefaultAsync(r => r.Nombre == dto.RolPrincipal && r.Activo, ct);
        if (rol is null) throw new InvalidOperationException($"Rol '{dto.RolPrincipal}' no existe o está inactivo.");

        var u = new Usuario
        {
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            Email = dto.Email,
            Clave = dto.Clave, 
            Telefono = dto.Telefono,
            Activo = dto.Activo,
            FechaAlta = DateTime.UtcNow
        };

        await _db.Usuarios.AddAsync(u, ct);
        await _db.SaveChangesAsync(ct); 

        if (dto.Direccion is not null)
        {
            var d = dto.Direccion;
            var dir = new Direccion
            {
                UsuarioId = u.Id,
                Calle = d.Calle,
                Numero = d.Numero,
                Ciudad = d.Ciudad,
                Provincia = d.Provincia,
                CodigoPostal = d.CodigoPostal,
                Pais = d.Pais
            };
            await _db.Direcciones.AddAsync(dir, ct);
        }

        await _db.UsuarioRoles.AddAsync(new UsuarioRol
        {
            UsuarioId = u.Id,
            RolId = rol.Id
        }, ct);

        await _db.SaveChangesAsync(ct);
        return u.Id;
    }

    public async Task UpdateAsync(int id, UsuarioSaveDto dto, CancellationToken ct)
    {
        var u = await _db.Usuarios.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (u is null) throw new KeyNotFoundException("Usuario no encontrado.");

        if (!string.Equals(u.Email, dto.Email, StringComparison.OrdinalIgnoreCase))
        {
            var emailExists = await _db.Usuarios.AnyAsync(x => x.Email == dto.Email && x.Id != id, ct);
            if (emailExists) throw new InvalidOperationException("El email ya está registrado por otro usuario.");
        }

        var rol = await _db.Roles.FirstOrDefaultAsync(r => r.Nombre == dto.RolPrincipal && r.Activo, ct);
        if (rol is null) throw new InvalidOperationException($"Rol '{dto.RolPrincipal}' no existe o está inactivo.");

        u.Nombre = dto.Nombre;
        u.Apellido = dto.Apellido;
        u.Email = dto.Email;
        u.Clave = dto.Clave; 
        u.Telefono = dto.Telefono;
        u.Activo = dto.Activo;

        var dir = await _db.Direcciones.FirstOrDefaultAsync(d => d.UsuarioId == id, ct);
        if (dto.Direccion is null)
        {
        }
        else
        {
            if (dir is null)
            {
                dir = new Direccion
                {
                    UsuarioId = id,
                    Calle = dto.Direccion.Calle,
                    Numero = dto.Direccion.Numero,
                    Ciudad = dto.Direccion.Ciudad,
                    Provincia = dto.Direccion.Provincia,
                    CodigoPostal = dto.Direccion.CodigoPostal,
                    Pais = dto.Direccion.Pais
                };
                await _db.Direcciones.AddAsync(dir, ct);
            }
            else
            {
                dir.Calle = dto.Direccion.Calle;
                dir.Numero = dto.Direccion.Numero;
                dir.Ciudad = dto.Direccion.Ciudad;
                dir.Provincia = dto.Direccion.Provincia;
                dir.CodigoPostal = dto.Direccion.CodigoPostal;
                dir.Pais = dto.Direccion.Pais;
            }
        }

        var actuales = await _db.UsuarioRoles.Where(ur => ur.UsuarioId == id).ToListAsync(ct);
        _db.UsuarioRoles.RemoveRange(actuales);
        await _db.UsuarioRoles.AddAsync(new UsuarioRol { UsuarioId = id, RolId = rol.Id }, ct);

        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct)
    {
        var u = await _db.Usuarios.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (u is null) return;

        _db.Usuarios.Remove(u); 
        await _db.SaveChangesAsync(ct);
    }
}
