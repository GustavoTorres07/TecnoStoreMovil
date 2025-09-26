using System.Net.Http.Json;
using TecnoStoreMovil.Services.Contrato;
using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Services.Implementa
{
    public class AdminPedidosClient : IAdminPedidosClient
    {
        private readonly IApiClient _api;
        public AdminPedidosClient(IApiClient api) => _api = api;

        public async Task<List<PedidoResumenDto>> ListAsync(string? estado)
        {
            var http = await _api.CreateAsync();
            var url = string.IsNullOrWhiteSpace(estado) ? "api/admin/pedidos" : $"api/admin/pedidos?estado={Uri.EscapeDataString(estado)}";
            var data = await http.GetFromJsonAsync<List<PedidoResumenDto>>(url);
            return data ?? new();
        }

        public async Task<PedidoDto?> GetAsync(int id)
        {
            var http = await _api.CreateAsync();
            return await http.GetFromJsonAsync<PedidoDto>($"api/admin/pedidos/{id}");
        }

        public async Task<bool> AceptarAsync(int id)
        {
            var http = await _api.CreateAsync();
            var resp = await http.PostAsync($"api/admin/pedidos/{id}/aceptar", null);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> RechazarAsync(int id, string? motivo)
        {
            var http = await _api.CreateAsync();
            var resp = await http.PostAsJsonAsync($"api/admin/pedidos/{id}/rechazar",
                new PedidoCambiarEstadoRequest("Cancelado", motivo));
            return resp.IsSuccessStatusCode;
        }
    }
}
