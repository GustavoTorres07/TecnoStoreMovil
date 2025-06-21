using System;
using System.Collections.Generic;
using System.Linq;
using TecnoStoreMovil.Models;

namespace TecnoStoreMovil.Services
{
    public class PedidoService
    {
        private readonly List<Pedido> pedidos;

        public PedidoService()
        {
            pedidos = new List<Pedido>();
            // Puedes precargar algunos pedidos de ejemplo aquí si quieres
        }

        // Obtener todos los pedidos
        public List<Pedido> GetPedidos() => pedidos;

        // Obtener un pedido por Id
        public Pedido? GetPedido(int id) => pedidos.FirstOrDefault(p => p.Id == id);

        // Agregar un nuevo pedido
        public void AddPedido(Pedido pedido)
        {
            pedido.Id = pedidos.Any() ? pedidos.Max(p => p.Id) + 1 : 1;
            pedido.Fecha = DateTime.Now;
            pedido.Estado = EstadoPedido.Pendiente;
            pedidos.Add(pedido);
        }

        // Actualizar un pedido existente (por ejemplo, cambiar estado)
        public bool UpdatePedido(Pedido pedido)
        {
            var index = pedidos.FindIndex(p => p.Id == pedido.Id);
            if (index == -1)
                return false;

            pedidos[index] = pedido;
            return true;
        }

        // Eliminar un pedido (opcional)
        public bool DeletePedido(int id)
        {
            var pedido = GetPedido(id);
            if (pedido == null)
                return false;

            pedidos.Remove(pedido);
            return true;
        }
    }
}
