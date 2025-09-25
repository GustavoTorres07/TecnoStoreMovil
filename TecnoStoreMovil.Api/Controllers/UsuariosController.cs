using Microsoft.AspNetCore.Mvc;
using TecnoStoreMovil.Api.Services.Contrato;
using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Api.Controllers;

[ApiController]
[Route("usuarios")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _svc;
    public UsuariosController(IUsuarioService svc) => _svc = svc;

    // GET /usuarios?q=torres
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioDto>>> Search([FromQuery] string? q, CancellationToken ct)
        => Ok(await _svc.SearchAsync(q, ct));

    // GET /usuarios/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UsuarioDto>> GetById(int id, CancellationToken ct)
    {
        var dto = await _svc.GetByIdAsync(id, ct);
        return dto is null ? NotFound() : Ok(dto);
    }

    // POST /usuarios
    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] UsuarioSaveDto dto, CancellationToken ct)
    {
        try
        {
            var id = await _svc.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }
        catch (InvalidOperationException ex) { return Conflict(ex.Message); }
    }

    // PUT /usuarios/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UsuarioSaveDto dto, CancellationToken ct)
    {
        try
        {
            await _svc.UpdateAsync(id, dto, ct);
            return NoContent();
        }
        catch (KeyNotFoundException) { return NotFound(); }
        catch (InvalidOperationException ex) { return Conflict(ex.Message); }
    }

    // DELETE /usuarios/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await _svc.DeleteAsync(id, ct);
        return NoContent();
    }
}
