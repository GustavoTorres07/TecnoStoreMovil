namespace TecnoStoreMovil.Services.Contrato
{
    public interface ISesionService
    {
        Task SetAsync(string sessionId, int userId, string rol, string nombre);
        Task<string?> GetSessionIdAsync();
        Task<string?> GetRolAsync();
        Task<string?> GetNombreAsync();
        Task<int?> GetUserIdAsync();   // <-- async
        void Logout();
        Task<bool> IsLoggedAsync();
    }

}
