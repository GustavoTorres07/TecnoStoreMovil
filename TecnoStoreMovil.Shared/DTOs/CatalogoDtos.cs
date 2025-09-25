using System.ComponentModel.DataAnnotations;

namespace TecnoStoreMovil.Shared.DTOs
{
    public record CategoriaDto(int Id, string Nombre, string? Descripcion, bool Activo);

    public record CategoriaSaveDto
    (
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres")]
        string Nombre,

        [StringLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres")]
        string? Descripcion,

        bool Activo
    );

    public record ProductoDto(
        int Id,
        int CategoriaId,
        string Categoria,
        string Nombre,
        string? Descripcion,
        decimal Precio,
        int Stock,
        string? ImagenUrl,
        bool Activo
    );

    public record ProductoSaveDto(
        int CategoriaId,
        string Nombre,
        string? Descripcion,
        decimal Precio,
        int Stock,
        string? ImagenUrl,
        bool Activo
    );
}