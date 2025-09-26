using Microsoft.Maui.Storage;
using TecnoStoreMovil.Services.Contrato;

namespace TecnoStoreMovil.Services.Implementa
{
    public class SesionService : ISesionService
    {
        private const string KSession = "session_id";
        private const string KUser = "user_id";
        private const string KRol = "rol";
        private const string KNombre = "nombre";
        //private const string KApellido = "apellido";

        public async Task SetAsync(string sessionId, int userId, string rol, string nombre/*, string apellido*/)
        {
            await SecureStorage.SetAsync(KSession, sessionId);
            await SecureStorage.SetAsync(KUser, userId.ToString());
            await SecureStorage.SetAsync(KRol, rol);
            await SecureStorage.SetAsync(KNombre, nombre);
            //await SecureStorage.SetAsync(KApellido, apellido);
        }

        public Task<string?> GetSessionIdAsync() => SecureStorage.GetAsync(KSession);
        public Task<string?> GetRolAsync() => SecureStorage.GetAsync(KRol);
        public Task<string?> GetNombreAsync() => SecureStorage.GetAsync(KNombre);
        //public Task<string?> GetApellidoAsync() => SecureStorage.GetAsync(KApellido);

        public async Task<int?> GetUserIdAsync()
        {
            try
            {
                var s = await SecureStorage.GetAsync(KUser);
                return int.TryParse(s, out var id) ? id : (int?)null;
            }
            catch
            {
                return null;
            }
        }

        public void Logout()
        {
            SecureStorage.Remove(KSession);
            SecureStorage.Remove(KUser);
            SecureStorage.Remove(KRol);
            SecureStorage.Remove(KNombre);
            //SecureStorage.Remove(KApellido);
        }

        public async Task<bool> IsLoggedAsync()
        {
            try
            {
                var sid = await GetSessionIdAsync();
                return !string.IsNullOrWhiteSpace(sid);
            }
            catch
            {
                return false;
            }
        }
    }
}
