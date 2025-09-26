using Microsoft.EntityFrameworkCore;
using TecnoStoreMovil.Api.Data;
using TecnoStoreMovil.Api.Services.Contrato;
using TecnoStoreMovil.Shared.DTOs;
using TecnoStoreMovil.Shared.Models;

namespace TecnoStoreMovil.Api.Services.Implementa
{
    public class PedidosService : IPedidosService
    {
        private readonly AppDbContext _db;
        private readonly ICarritoService _carritos;

        public PedidosService(AppDbContext db, ICarritoService carritos)
        {
            _db = db;
            _carritos = carritos;
        }

        public async Task<int?> CrearDesdeCarritoAsync(int usuarioId, CancellationToken ct = default)
        {
            var cart = await _carritos.ObtenerCarritoAbiertoAsync(usuarioId, ct);
            if (cart is null || cart.Items.Count == 0) return null;

            using var tx = await _db.Database.BeginTransactionAsync(ct);

            var pedido = new Pedido
            {
                UsuarioId = usuarioId,
                FechaCreacion = DateTime.UtcNow,
                Estado = EstadoPedido.Pendiente,
                Total = 0
            };

            _db.Pedidos.Add(pedido);
            await _db.SaveChangesAsync(ct);

            var items = new List<PedidoItem>();
            foreach (var ci in cart.Items)
            {
                var prod = await _db.Productos.FindAsync(new object?[] { ci.ProductoId }, ct);
                if (prod is null) continue;

                items.Add(new PedidoItem
                {
                    PedidoId = pedido.Id,
                    ProductoId = prod.Id,
                    Cantidad = ci.Cantidad,
                    PrecioUnit = ci.PrecioUnit
                });
            }

            if (items.Count == 0) return null;

            _db.PedidoItems.AddRange(items);
            await _db.SaveChangesAsync(ct);

            pedido.Total = items.Sum(i => i.Cantidad * i.PrecioUnit);
            await _db.SaveChangesAsync(ct);

            _db.CarritoItems.RemoveRange(cart.Items);
            cart.Estado = EstadoCarrito.Cerrado;
            await _db.SaveChangesAsync(ct);

            await tx.CommitAsync(ct);
            return pedido.Id;
        }

        public async Task<List<PedidoResumenDto>> MisPedidosAsync(int usuarioId, string? estado, CancellationToken ct = default)
        {
            var q = _db.Pedidos.Where(p => p.UsuarioId == usuarioId);
            if (!string.IsNullOrWhiteSpace(estado))
                q = q.Where(p => p.Estado == estado);

            return await q.OrderByDescending(p => p.FechaCreacion)
                .Select(p => new PedidoResumenDto(
                    p.Id,
                    p.FechaCreacion,
                    p.Estado,
                    p.Total,
                    null, 
                    null  
                ))
                .ToListAsync(ct);
        }

        public async Task<PedidoDto?> ObtenerDetalleAsync(int pedidoId, CancellationToken ct = default)
        {
            var cab = await _db.Pedidos.FirstOrDefaultAsync(p => p.Id == pedidoId, ct);
            if (cab is null) return null;

            var items = await _db.PedidoItems
                .Include(i => i.Producto)
                .Where(i => i.PedidoId == pedidoId)
                .Select(i => new PedidoItemLineaDto(
                    i.ProductoId,
                    i.Producto!.Nombre,
                    i.PrecioUnit,
                    i.Cantidad,
                    i.Cantidad * i.PrecioUnit
                ))
                .ToListAsync(ct);

            return new PedidoDto(
                cab.Id,
                cab.FechaCreacion,
                cab.Estado,
                cab.Total,
                items,
                null,  
                null   
            );
        }
    }
}