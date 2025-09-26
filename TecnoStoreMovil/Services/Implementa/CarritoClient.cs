using System.Net.Http.Json;
using TecnoStoreMovil.Services.Contrato;
using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Services.Implementa
{
    public class CarritoClient : ICarritoClient
    {
        private readonly IApiClient _api;
        private readonly ISesionService _sesion;

        public CarritoClient(IApiClient api, ISesionService sesion)
        {
            _api = api;
            _sesion = sesion;
        }

        private async Task<int?> GetUserIdAsync() => await _sesion.GetUserIdAsync();

        public async Task<CarritoDto?> GetAsync()
        {
            var uid = await GetUserIdAsync();
            if (uid is null) return null;

            var http = await _api.CreateAsync();
            return await http.GetFromJsonAsync<CarritoDto>($"api/carrito?usuarioId={uid.Value}");
        }

        public async Task<bool> AddItemAsync(int productoId, int cantidad)
        {
            var uid = await GetUserIdAsync();
            if (uid is null) return false;

            var http = await _api.CreateAsync();
            var resp = await http.PostAsJsonAsync("api/carrito/items", new CarritoAddItemRequest(uid.Value, productoId, cantidad));
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateQtyAsync(int productoId, int cantidad)
        {
            var uid = await GetUserIdAsync();
            if (uid is null) return false;

            var http = await _api.CreateAsync();
            var resp = await http.PutAsJsonAsync("api/carrito/items", new CarritoUpdateQtyRequest(uid.Value, productoId, cantidad));
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveItemAsync(int productoId)
        {
            var uid = await GetUserIdAsync();
            if (uid is null) return false;

            var http = await _api.CreateAsync();
            var resp = await http.DeleteAsync($"api/carrito/items/{uid.Value}/{productoId}");
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> ClearAsync()
        {
            var uid = await GetUserIdAsync();
            if (uid is null) return false;

            var http = await _api.CreateAsync();
            var resp = await http.DeleteAsync($"api/carrito/{uid.Value}");
            return resp.IsSuccessStatusCode;
        }
    }
}
