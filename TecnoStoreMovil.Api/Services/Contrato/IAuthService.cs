namespace TecnoStoreMovil.Api.Services.Contrato
{
    public interface IAuthService
    {
        Task<(bool Ok, int UsuarioId, string Nombre, string Rol)> LoginAsync(string email, string clave, CancellationToken ct);
    }
}
