using System.Net.Http.Json;
using TecnoStoreMovil.Services.Contrato;
using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Services.Implementa
{
    public class PedidosClient : IPedidosClient
    {
        private readonly IApiClient _api;
        public PedidosClient(IApiClient api) => _api = api;

        public async Task<int?> CrearDesdeCarritoAsync(int usuarioId)
        {
            var http = await _api.CreateAsync();
            var resp = await http.PostAsJsonAsync("api/pedidos/crear-desde-carrito", new PedidoCrearDesdeCarritoRequest(usuarioId));
            if (!resp.IsSuccessStatusCode) return null;
            return await resp.Content.ReadFromJsonAsync<int>();
        }

        public async Task<List<PedidoResumenDto>> MisPedidosAsync(int usuarioId, string? estado = null)
        {
            var http = await _api.CreateAsync();
            var url = string.IsNullOrWhiteSpace(estado)
                ? $"api/pedidos/usuario/{usuarioId}"
                : $"api/pedidos/usuario/{usuarioId}?estado={Uri.EscapeDataString(estado)}";

            var data = await http.GetFromJsonAsync<List<PedidoResumenDto>>(url);
            return data ?? new();
        }

        public async Task<PedidoDto?> GetDetalleAsync(int pedidoId)
        {
            var http = await _api.CreateAsync();
            return await http.GetFromJsonAsync<PedidoDto>($"api/pedidos/{pedidoId}");
        }
    }
}
