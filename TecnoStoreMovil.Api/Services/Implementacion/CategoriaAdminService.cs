// CategoriaAdminService.cs (ABM)
using Microsoft.EntityFrameworkCore;
using TecnoStoreMovil.Api.Data;
using TecnoStoreMovil.Api.Services.Contrato;
using TecnoStoreMovil.Shared.DTOs;
using TecnoStoreMovil.Shared.Models;

namespace TecnoStoreMovil.Api.Services.Implementacion;

public class CategoriaAdminService : ICategoriaAdminService
{
    private readonly AppDbContext _db;
    public CategoriaAdminService(AppDbContext db) => _db = db;

    public async Task<int> CreateAsync(CategoriaSaveDto dto, CancellationToken ct)
    {
        var name = (dto.Nombre ?? "").Trim();
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidOperationException("El nombre es obligatorio.");

        var exists = await _db.Categorias.AnyAsync(c => c.Nombre.ToLower() == name.ToLower(), ct);
        if (exists) throw new InvalidOperationException("Ya existe una categoría con ese nombre.");

        var entity = new Categoria { Nombre = name, Descripcion = dto.Descripcion, Activo = dto.Activo };
        _db.Categorias.Add(entity);
        await _db.SaveChangesAsync(ct);
        return entity.Id;
    }

    public async Task UpdateAsync(int id, CategoriaSaveDto dto, CancellationToken ct)
    {
        var entity = await _db.Categorias.FirstOrDefaultAsync(c => c.Id == id, ct);
        if (entity is null) throw new KeyNotFoundException("Categoría no encontrada.");

        var name = (dto.Nombre ?? "").Trim();
        if (!string.Equals(entity.Nombre, name, StringComparison.OrdinalIgnoreCase))
        {
            var dup = await _db.Categorias.AnyAsync(c => c.Id != id && c.Nombre.ToLower() == name.ToLower(), ct);
            if (dup) throw new InvalidOperationException("Ya existe una categoría con ese nombre.");
        }

        entity.Nombre = name;
        entity.Descripcion = dto.Descripcion;
        entity.Activo = dto.Activo;
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct)
    {
        var entity = await _db.Categorias.FirstOrDefaultAsync(c => c.Id == id, ct);
        if (entity is null) return;

        _db.Categorias.Remove(entity);
        await _db.SaveChangesAsync(ct);
    }
}
