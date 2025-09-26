namespace TecnoStoreMovil.Shared.Models
{
    public class Direccion
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }        
        public string Calle { get; set; } = string.Empty;
        public string? Numero { get; set; }
        public string Ciudad { get; set; } = string.Empty;
        public string Provincia { get; set; } = string.Empty;
        public string CodigoPostal { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;

        public Usuario Usuario { get; set; } = null!;
    }
}
