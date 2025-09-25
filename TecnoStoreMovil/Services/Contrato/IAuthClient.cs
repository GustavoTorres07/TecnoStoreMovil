using System.Threading.Tasks;

namespace TecnoStoreMovil.Services.Contrato;

public interface IAuthClient
{
    Task<bool> LoginAsync(string email, string clave);
    void Logout();
}
