using Microsoft.AspNetCore.Mvc;
using TecnoStoreMovil.Api.Services.Contrato;
using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Api.Controllers
{
    [ApiController]
    [Route("api/carrito")]
    public class CarritoController : ControllerBase
    {
        private readonly ICarritoService _svc;
        public CarritoController(ICarritoService svc) => _svc = svc;

        [HttpGet]
        public async Task<ActionResult<CarritoDto?>> Get([FromQuery] int usuarioId, CancellationToken ct)
            => Ok(await _svc.ObtenerDtoAsync(usuarioId, ct));

        [HttpPost("items")]
        public async Task<ActionResult> AddItem([FromBody] CarritoAddItemRequest body, CancellationToken ct)
            => await _svc.AgregarItemAsync(body.UsuarioId, body.ProductoId, body.Cantidad, ct) ? Ok() : BadRequest();

        [HttpPut("items")]
        public async Task<ActionResult> UpdateQty([FromBody] CarritoUpdateQtyRequest body, CancellationToken ct)
            => await _svc.ActualizarCantidadAsync(body.UsuarioId, body.ProductoId, body.Cantidad, ct) ? Ok() : BadRequest();

        [HttpDelete("items/{usuarioId:int}/{productoId:int}")]
        public async Task<ActionResult> Remove(int usuarioId, int productoId, CancellationToken ct)
            => await _svc.EliminarItemAsync(usuarioId, productoId, ct) ? Ok() : NotFound();

        [HttpDelete("{usuarioId:int}")]
        public async Task<ActionResult> Clear(int usuarioId, CancellationToken ct)
            => await _svc.VaciarAsync(usuarioId, ct) ? Ok() : BadRequest();
    }
}
