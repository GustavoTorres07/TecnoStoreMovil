namespace TecnoStoreMovil.Shared.DTOs
{
    public record LoginRequest(string Email, string Clave);


    public record LoginResponse(string SessionId, int UsuarioId, string Rol, string Nombre/*, string Apellido*/);
}
