using Microsoft.AspNetCore.Mvc;
using TecnoStoreMovil.Api.Services.Contrato;
using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Api.Controllers;

[ApiController]
[Route("productos")]
public class ProductosController : ControllerBase
{
    private readonly IProductoService _read;
    private readonly IProductoAdminService _admin;

    public ProductosController(IProductoService read, IProductoAdminService admin)
    {
        _read = read; _admin = admin;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductoDto>>> Get([FromQuery] int? categoriaId, [FromQuery] string? q, CancellationToken ct)
        => Ok(await _read.SearchAsync(categoriaId, q, ct));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductoDto>> GetById(int id, CancellationToken ct)
    {
        var dto = await _read.GetByIdAsync(id, ct);
        return dto is null ? NotFound() : Ok(dto);
    }

    // ABM
    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] ProductoSaveDto dto, CancellationToken ct)
    {
        try
        {
            var id = await _admin.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }
        catch (InvalidOperationException ex) { return Conflict(ex.Message); }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductoSaveDto dto, CancellationToken ct)
    {
        try { await _admin.UpdateAsync(id, dto, ct); return NoContent(); }
        catch (KeyNotFoundException) { return NotFound(); }
        catch (InvalidOperationException ex) { return Conflict(ex.Message); }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await _admin.DeleteAsync(id, ct);
        return NoContent();
    }
}
