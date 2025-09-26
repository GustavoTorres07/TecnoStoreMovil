namespace TecnoStoreMovil.Services.Contrato
{
    public interface ISesionService
    {
        Task SetAsync(string sessionId, int userId, string rol, string nombre/*, string apellido*/);
        Task<string?> GetSessionIdAsync();
        Task<string?> GetRolAsync();
        Task<string?> GetNombreAsync();
        //Task<string?> GetApellidoAsync();
        Task<int?> GetUserIdAsync();  
        void Logout();
        Task<bool> IsLoggedAsync();
    }

}
