namespace TecnoStoreMovil.Shared.DTOs
{
    public record UsuarioDto(
        int Id,
        string Nombre,
        string Apellido,
        string Email,
        string? Telefono,
        bool Activo,
        DateTime FechaAlta,
        DireccionDto? Direccion,
        string[] Roles
    );

    public record UsuarioSaveDto(
        string Nombre,
        string Apellido,
        string Email,
        string Clave,
        string? Telefono,
        bool Activo,
        DireccionSaveDto? Direccion, 
        string RolPrincipal          
    );

    public record DireccionDto(
        int Id,
        int UsuarioId,
        string Calle,
        string? Numero,
        string Ciudad,
        string Provincia,
        string CodigoPostal,
        string Pais
    );

    public record DireccionSaveDto(
        string Calle,
        string? Numero,
        string Ciudad,
        string Provincia,
        string CodigoPostal,
        string Pais
    );

    public record RolDto(int Id, string Nombre, string? Descripcion, bool Activo);
}
