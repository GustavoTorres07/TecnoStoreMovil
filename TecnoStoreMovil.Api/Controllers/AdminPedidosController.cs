using Microsoft.AspNetCore.Mvc;
using TecnoStoreMovil.Api.Services.Contrato;
using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Api.Controllers
{
    [ApiController]
    [Route("api/admin/pedidos")]
    public class AdminPedidosController : ControllerBase
    {
        private readonly IAdminPedidosService _svc;
        public AdminPedidosController(IAdminPedidosService svc) => _svc = svc;

        [HttpGet]
        public async Task<ActionResult<List<PedidoResumenDto>>> List([FromQuery] string? estado, CancellationToken ct)
            => Ok(await _svc.ListAsync(estado, ct));

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PedidoDto?>> Get(int id, CancellationToken ct)
        {
            var dto = await _svc.GetAsync(id, ct);
            return dto is null ? NotFound() : Ok(dto);
        }

        [HttpPost("{id:int}/aceptar")]
        public async Task<ActionResult> Aceptar(int id, CancellationToken ct)
            => await _svc.AceptarAsync(id, ct) ? Ok() : BadRequest("Sin stock o estado inválido.");

        [HttpPost("{id:int}/rechazar")]
        public async Task<ActionResult> Rechazar(int id, [FromBody] PedidoCambiarEstadoRequest body, CancellationToken ct)
            => await _svc.RechazarAsync(id, body.Motivo, ct) ? Ok() : BadRequest("Estado inválido.");
    }
}
