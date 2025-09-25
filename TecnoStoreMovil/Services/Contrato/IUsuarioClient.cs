using TecnoStoreMovil.Shared.DTOs;

namespace TecnoStoreMovil.Services.Contrato;

public interface IUsuariosClient
{
    Task<List<UsuarioDto>> SearchAsync(string? q = null);
    Task<UsuarioDto?> GetByIdAsync(int id);
    Task<int?> CreateAsync(UsuarioSaveDto dto);
    Task<bool> UpdateAsync(int id, UsuarioSaveDto dto);
    Task<bool> DeleteAsync(int id);
}
