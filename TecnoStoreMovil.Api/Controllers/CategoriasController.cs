using Microsoft.AspNetCore.Mvc;
using TecnoStoreMovil.Api.Services.Contrato;
using TecnoStoreMovil.Shared.DTOs;

[ApiController]
[Route("categorias")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _svc;
    private readonly ICategoriaAdminService _adminSvc;

    public CategoriasController(ICategoriaService svc, ICategoriaAdminService adminSvc)
    {
        _svc = svc;
        _adminSvc = adminSvc;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaDto>>> GetAll(CancellationToken ct)
        => Ok(await _svc.GetAllAsync(ct));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoriaDto>> GetById(int id, CancellationToken ct)
    {
        var dto = await _svc.GetByIdAsync(id, ct);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]   // 👈 igual que en Usuarios
    public async Task<ActionResult<int>> Create([FromBody] CategoriaSaveDto dto, CancellationToken ct)
    {
        try
        {
            var id = await _adminSvc.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }
        catch (InvalidOperationException ex) { return Conflict(ex.Message); }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CategoriaSaveDto dto, CancellationToken ct)
    {
        try { await _adminSvc.UpdateAsync(id, dto, ct); return NoContent(); }
        catch (KeyNotFoundException) { return NotFound(); }
        catch (InvalidOperationException ex) { return Conflict(ex.Message); }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await _adminSvc.DeleteAsync(id, ct);
        return NoContent();
    }
}
