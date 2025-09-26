using Microsoft.EntityFrameworkCore;
using TecnoStoreMovil.Api.Data;
using TecnoStoreMovil.Api.Services.Contrato;
using TecnoStoreMovil.Shared.DTOs;
using TecnoStoreMovil.Shared.Models;

namespace TecnoStoreMovil.Api.Services.Implementa
{
    public class AdminPedidosService : IAdminPedidosService
    {
        private readonly AppDbContext _db;
        public AdminPedidosService(AppDbContext db) => _db = db;

        public async Task<List<PedidoResumenDto>> ListAsync(string? estado, CancellationToken ct = default)
        {
            var q = _db.Pedidos.Include(p => p.Usuario).AsQueryable();
            if (!string.IsNullOrWhiteSpace(estado))
                q = q.Where(p => p.Estado == estado);
            return await q
                .OrderByDescending(p => p.FechaCreacion)
                .Select(p => new PedidoResumenDto(
                    p.Id,
                    p.FechaCreacion,
                    p.Estado,
                    p.Total,
                    p.Usuario.Nombre + " " + p.Usuario.Apellido,
                    p.UsuarioId
                ))
                .ToListAsync(ct);
        }

        public async Task<PedidoDto?> GetAsync(int id, CancellationToken ct = default)
        {
            var cab = await _db.Pedidos.Include(p => p.Usuario).FirstOrDefaultAsync(p => p.Id == id, ct);
            if (cab is null) return null;
            var items = await _db.PedidoItems
                .Include(i => i.Producto)
                .Where(i => i.PedidoId == id)
                .Select(i => new PedidoItemLineaDto(
                    i.ProductoId,
                    i.Producto!.Nombre,
                    i.PrecioUnit,
                    i.Cantidad,
                    i.Cantidad * i.PrecioUnit))
                .ToListAsync(ct);
            return new PedidoDto(
                cab.Id,
                cab.FechaCreacion,
                cab.Estado,
                cab.Total,
                items,
                cab.Usuario.Nombre + " " + cab.Usuario.Apellido,
                cab.UsuarioId
            );
        }

        public async Task<bool> AceptarAsync(int id, CancellationToken ct = default)
        {
            using var tx = await _db.Database.BeginTransactionAsync(ct);
            var pedido = await _db.Pedidos
                .Include(p => p.Items)
                .ThenInclude(i => i.Producto)
                .FirstOrDefaultAsync(p => p.Id == id, ct);

            if (pedido is null || pedido.Estado != EstadoPedido.Pendiente) return false;

            // Control de stock
            foreach (var it in pedido.Items)
            {
                if (it.Producto is null) return false;
                if (it.Producto.Stock < it.Cantidad) return false;
                it.Producto.Stock -= it.Cantidad;
            }

            pedido.Estado = EstadoPedido.Aprobado;

            await _db.SaveChangesAsync(ct);
            await tx.CommitAsync(ct);
            return true;
        }

        public async Task<bool> RechazarAsync(int id, string? motivo, CancellationToken ct = default)
        {
            var pedido = await _db.Pedidos.FirstOrDefaultAsync(p => p.Id == id, ct);
            if (pedido is null || pedido.Estado != EstadoPedido.Pendiente) return false;

            pedido.Estado = EstadoPedido.Cancelado;

            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}