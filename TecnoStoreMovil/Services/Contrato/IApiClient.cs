namespace TecnoStoreMovil.Services.Contrato
{
    public interface IApiClient
    {
        Task<HttpClient> CreateAsync();
    }
}
