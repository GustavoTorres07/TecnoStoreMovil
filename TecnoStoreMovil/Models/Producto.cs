using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecnoStoreMovil.Models
{
        public class Producto
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "El Nombre es obligatorio.")]
            public string Nombre { get; set; }

            public string Descripcion { get; set; }

            [Range(0.01, double.MaxValue, ErrorMessage = "El Precio debe ser mayor que 0.")]
            public decimal Precio { get; set; } = 0.0m;

            public string ImagenProducto { get; set; }

            [Range(1, int.MaxValue, ErrorMessage = "La Cantidad debe ser mayor que 0.")]
            public int Cantidad { get; set; }

            [Required(ErrorMessage = "El Stock es obligatorio.")]
            public bool Stock { get; set; }

            [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una categoría.")]
            public int CategoriaId { get; set; }
            public Categoria? Categoria { get; set; }

            // 🎮 Propiedades exclusivas para productos de categoría "Juegos"
            public PlataformaJuego? Plataforma { get; set; } // Enum opcional
            public DateTime? FechaLanzamiento { get; set; }
        }
}
