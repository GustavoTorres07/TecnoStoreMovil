// CategoriaService.cs (lectura)
using Microsoft.EntityFrameworkCore;
using TecnoStoreMovil.Api.Data;
using TecnoStoreMovil.Api.Services.Contrato;
using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Api.Services.Implementa;

public class CategoriaService : ICategoriaService
{
    private readonly AppDbContext _db;
    public CategoriaService(AppDbContext db) => _db = db;

    public async Task<IReadOnlyList<CategoriaDto>> GetAllAsync(CancellationToken ct)
        => await _db.Categorias.AsNoTracking()
            .OrderBy(c => c.Nombre)
            .Select(c => new CategoriaDto(c.Id, c.Nombre, c.Descripcion, c.Activo))
            .ToListAsync(ct);

    public async Task<CategoriaDto?> GetByIdAsync(int id, CancellationToken ct)
        => await _db.Categorias.AsNoTracking()
            .Where(c => c.Id == id)
            .Select(c => new CategoriaDto(c.Id, c.Nombre, c.Descripcion, c.Activo))
            .FirstOrDefaultAsync(ct);
}
