﻿namespace TecnoStoreMovil.Shared.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }

        // Nav
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
