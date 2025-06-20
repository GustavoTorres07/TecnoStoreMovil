using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecnoStoreMovil.Models;

namespace TecnoStoreMovil.Services
{
    public class CarritoService
    {
        private readonly List<ItemCarrito> items = new();

        public IReadOnlyList<ItemCarrito> ObtenerItems() => items;

        public void AgregarItem(Producto producto, int cantidad)
        {
            var existente = items.FirstOrDefault(i => i.Producto.Id == producto.Id);
            if (existente != null)
                existente.Cantidad += cantidad;
            else
                items.Add(new ItemCarrito { Producto = producto, Cantidad = cantidad });
        }

        public void EliminarItem(int productoId)
        {
            var item = items.FirstOrDefault(i => i.Producto.Id == productoId);
            if (item != null)
                items.Remove(item);
        }

        public void LimpiarCarrito()
        {
            items.Clear();
        }

        public decimal CalcularTotal()
        {
            return items.Sum(i => i.Producto.Precio * i.Cantidad);
        }
    }

    public class ItemCarrito
    {
        public Producto Producto { get; set; } = null!;
        public int Cantidad { get; set; }
    }
}
