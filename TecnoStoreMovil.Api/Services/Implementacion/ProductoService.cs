using Microsoft.EntityFrameworkCore;
using TecnoStoreMovil.Api.Data;
using TecnoStoreMovil.Api.Services.Contrato;
using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Api.Services.Implementacion;

public class ProductoService : IProductoService
{
    private readonly AppDbContext _db;
    public ProductoService(AppDbContext db) => _db = db;

    public async Task<IReadOnlyList<ProductoDto>> SearchAsync(int? categoriaId, string? q, CancellationToken ct)
    {
        var qry = _db.Productos.AsNoTracking().Where(p => p.Activo);

        if (categoriaId.HasValue && categoriaId.Value > 0)
            qry = qry.Where(p => p.CategoriaId == categoriaId.Value);

        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.Trim();
            qry = qry.Where(p => p.Nombre.Contains(term) || (p.Descripcion != null && p.Descripcion.Contains(term)));
        }

        return await qry
            .OrderBy(p => p.Nombre)
            .Select(p => new ProductoDto(
                p.Id, p.CategoriaId, p.Categoria.Nombre, p.Nombre, p.Descripcion,
                p.Precio, p.Stock, p.ImagenUrl, p.Activo))
            .ToListAsync(ct);
    }

    public async Task<ProductoDto?> GetByIdAsync(int id, CancellationToken ct)
        => await _db.Productos.AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new ProductoDto(
                p.Id, p.CategoriaId, p.Categoria.Nombre, p.Nombre, p.Descripcion,
                p.Precio, p.Stock, p.ImagenUrl, p.Activo))
            .FirstOrDefaultAsync(ct);
}
