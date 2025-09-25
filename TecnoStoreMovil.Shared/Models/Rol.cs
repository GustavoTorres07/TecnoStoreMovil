namespace TecnoStoreMovil.Shared.Models
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;     
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }
        public ICollection<UsuarioRol> UsuarioRoles { get; set; } = new List<UsuarioRol>();
    }
}
