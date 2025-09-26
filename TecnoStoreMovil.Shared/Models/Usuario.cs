namespace TecnoStoreMovil.Shared.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Clave { get; set; } = string.Empty;  
        public string? Telefono { get; set; }
        public DateTime FechaAlta { get; set; }
        public bool Activo { get; set; }

        public Direccion? Direccion { get; set; }

        public ICollection<UsuarioRol> UsuarioRoles { get; set; } = new List<UsuarioRol>();

        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

        public ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();

    }
}
