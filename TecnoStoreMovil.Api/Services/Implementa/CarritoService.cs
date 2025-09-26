using Microsoft.EntityFrameworkCore;
using TecnoStoreMovil.Api.Data;
using TecnoStoreMovil.Api.Services.Contrato;
using TecnoStoreMovil.Shared.DTOs;
using TecnoStoreMovil.Shared.Models;
using CartDto = TecnoStoreMovil.Shared.DTOs.CarritoDto;
using CartItemDto = TecnoStoreMovil.Shared.DTOs.CarritoItemDto;

namespace TecnoStoreMovil.Api.Services.Implementa;

public class CarritoService : ICarritoService
{
    private readonly AppDbContext _db;
    public CarritoService(AppDbContext db) => _db = db;

    public async Task<Carrito?> ObtenerCarritoAbiertoAsync(int usuarioId, CancellationToken ct = default)
        => await _db.Carritos
            .Include(c => c.Items)
            .ThenInclude(i => i.Producto)
            .Where(c => c.UsuarioId == usuarioId && c.Estado == EstadoCarrito.Abierto)
            .FirstOrDefaultAsync(ct);

    public async Task<Carrito> ObtenerOCrearCarritoAbiertoAsync(int usuarioId, CancellationToken ct = default)
    {
        var cart = await ObtenerCarritoAbiertoAsync(usuarioId, ct);
        if (cart is not null) return cart;

        cart = new Carrito { UsuarioId = usuarioId, Estado = EstadoCarrito.Abierto, FechaCreacion = DateTime.UtcNow };
        _db.Carritos.Add(cart);
        await _db.SaveChangesAsync(ct);
        return cart;
    }

    public async Task<CartDto?> ObtenerDtoAsync(int usuarioId, CancellationToken ct = default)
    {
        var cart = await ObtenerCarritoAbiertoAsync(usuarioId, ct);
        if (cart is null) return new CartDto(0, usuarioId, new List<CartItemDto>(), 0);

        var items = cart.Items.Select(i =>
        {
            var stockDisp = i.Producto?.Stock ?? 0;
            var subtotal = i.Cantidad * i.PrecioUnit;
            return new CartItemDto(
                i.ProductoId,
                i.Producto?.Nombre ?? "",
                i.Producto?.ImagenUrl,
                i.PrecioUnit,
                i.Cantidad,
                stockDisp,
                subtotal
            );
        }).ToList(); 

        var total = items.Sum(x => x.Subtotal);
        return new CartDto(cart.Id, cart.UsuarioId, items, total);
    }

    public async Task<bool> AgregarItemAsync(int usuarioId, int productoId, int cantidad, CancellationToken ct = default)
    {
        if (cantidad <= 0) return false;
        var cart = await ObtenerOCrearCarritoAbiertoAsync(usuarioId, ct);
        var prod = await _db.Productos.FindAsync(new object?[] { productoId }, ct);
        if (prod is null || !prod.Activo) return false;

        var existing = await _db.CarritoItems
            .FirstOrDefaultAsync(i => i.CarritoId == cart.Id && i.ProductoId == productoId, ct);

        if (existing is null)
        {
            _db.CarritoItems.Add(new CarritoItem
            {
                CarritoId = cart.Id,
                ProductoId = productoId,
                Cantidad = cantidad,
                PrecioUnit = prod.Precio
            });
        }
        else
        {
            existing.Cantidad += cantidad;
        }

        await _db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> ActualizarCantidadAsync(int usuarioId, int productoId, int cantidad, CancellationToken ct = default)
    {
        if (cantidad <= 0) return false;
        var cart = await ObtenerCarritoAbiertoAsync(usuarioId, ct);
        if (cart is null) return false;

        var item = await _db.CarritoItems
            .FirstOrDefaultAsync(i => i.CarritoId == cart.Id && i.ProductoId == productoId, ct);

        if (item is null) return false;
        item.Cantidad = cantidad;
        await _db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> EliminarItemAsync(int usuarioId, int productoId, CancellationToken ct = default)
    {
        var cart = await ObtenerCarritoAbiertoAsync(usuarioId, ct);
        if (cart is null) return false;

        var item = await _db.CarritoItems
            .FirstOrDefaultAsync(i => i.CarritoId == cart.Id && i.ProductoId == productoId, ct);

        if (item is null) return false;

        _db.CarritoItems.Remove(item);
        await _db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> VaciarAsync(int usuarioId, CancellationToken ct = default)
    {
        var cart = await ObtenerCarritoAbiertoAsync(usuarioId, ct);
        if (cart is null) return true;

        _db.CarritoItems.RemoveRange(cart.Items);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}
