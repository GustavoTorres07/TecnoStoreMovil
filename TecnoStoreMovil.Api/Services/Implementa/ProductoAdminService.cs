using Microsoft.EntityFrameworkCore;
using TecnoStoreMovil.Api.Data;
using TecnoStoreMovil.Api.Services.Contrato;
using TecnoStoreMovil.Shared.DTOs;
using TecnoStoreMovil.Shared.Models;

namespace TecnoStoreMovil.Api.Services.Implementa;

public class ProductoAdminService : IProductoAdminService
{
    private readonly AppDbContext _db;
    public ProductoAdminService(AppDbContext db) => _db = db;

    public async Task<int> CreateAsync(ProductoSaveDto dto, CancellationToken ct)
    {
        var catExists = await _db.Categorias.AnyAsync(c => c.Id == dto.CategoriaId, ct);
        if (!catExists) throw new InvalidOperationException("La categoría indicada no existe.");

        var nameExists = await _db.Productos.AnyAsync(p => p.Nombre == dto.Nombre, ct);
        if (nameExists) throw new InvalidOperationException("Ya existe un producto con ese nombre.");

        var p = new Producto
        {
            CategoriaId = dto.CategoriaId,
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            Precio = dto.Precio,
            Stock = dto.Stock,
            ImagenUrl = dto.ImagenUrl,
            Activo = dto.Activo
        };

        _db.Productos.Add(p);
        await _db.SaveChangesAsync(ct);
        return p.Id;
    }

    public async Task UpdateAsync(int id, ProductoSaveDto dto, CancellationToken ct)
    {
        var p = await _db.Productos.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (p is null) throw new KeyNotFoundException("Producto no encontrado.");

        var catExists = await _db.Categorias.AnyAsync(c => c.Id == dto.CategoriaId, ct);
        if (!catExists) throw new InvalidOperationException("La categoría indicada no existe.");

        if (!string.Equals(p.Nombre, dto.Nombre, StringComparison.OrdinalIgnoreCase))
        {
            var exists = await _db.Productos.AnyAsync(x => x.Nombre == dto.Nombre && x.Id != id, ct);
            if (exists) throw new InvalidOperationException("Ya existe un producto con ese nombre.");
        }

        p.CategoriaId = dto.CategoriaId;
        p.Nombre = dto.Nombre;
        p.Descripcion = dto.Descripcion;
        p.Precio = dto.Precio;
        p.Stock = dto.Stock;
        p.ImagenUrl = dto.ImagenUrl;
        p.Activo = dto.Activo;

        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct)
    {
        var p = await _db.Productos.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (p is null) return;

        _db.Productos.Remove(p);
        await _db.SaveChangesAsync(ct);
    }
}
