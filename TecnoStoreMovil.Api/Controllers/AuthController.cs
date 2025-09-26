using Microsoft.AspNetCore.Mvc;
using TecnoStoreMovil.Api.Services.Contrato;
using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;
    public AuthController(IAuthService auth) => _auth = auth;

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest req, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(req.Email) || string.IsNullOrWhiteSpace(req.Clave))
            return BadRequest("Email y clave son obligatorios.");

        var (ok, userId, nombre, rol) = await _auth.LoginAsync(req.Email, req.Clave, ct);
        if (!ok) return Unauthorized("Credenciales inválidas o usuario inactivo.");

        var sessionId = Guid.NewGuid().ToString("N"); 
        return Ok(new LoginResponse(sessionId, userId, rol, nombre));
    }
}
