using Microsoft.AspNetCore.Mvc;
using TecnoStoreMovil.Api.Services.Contrato;
using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Api.Controllers
{
    [ApiController]
    [Route("api/pedidos")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidosService _svc;
        public PedidosController(IPedidosService svc) => _svc = svc;

        [HttpPost("crear-desde-carrito")]
        public async Task<ActionResult<int>> CrearDesdeCarrito([FromBody] PedidoCrearDesdeCarritoRequest req, CancellationToken ct)
        {
            var id = await _svc.CrearDesdeCarritoAsync(req.UsuarioId, ct);
            return id is null ? BadRequest() : Ok(id.Value);
        }

        [HttpGet("usuario/{usuarioId:int}")]
        public async Task<ActionResult<List<PedidoResumenDto>>> MisPedidos(
            int usuarioId, [FromQuery] string? estado, CancellationToken ct)
            => Ok(await _svc.MisPedidosAsync(usuarioId, estado, ct));

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PedidoDto?>> Get(int id, CancellationToken ct)
        {
            var dto = await _svc.ObtenerDetalleAsync(id, ct);
            return dto is null ? NotFound() : Ok(dto);
        }
    }
}
