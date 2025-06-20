using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TecnoStoreMovil.Models;

namespace TecnoStoreMovil.Services
{
    public class CategoriaService
    {
        private readonly List<Categoria> categorias;

        public CategoriaService()
        {
            categorias = new List<Categoria>
            {
                new Categoria { Id = 1, Nombre = "Consolas", Descripcion = "Videoconsolas", Activo = true },
                new Categoria { Id = 2, Nombre = "Accesorios", Descripcion = "Periféricos y accesorios", Activo = true },
                new Categoria { Id = 3, Nombre = "Juegos", Descripcion = "Juegos por plataforma", Activo = true },
            };
        }

        public List<Categoria> ObtenerTodas() => categorias;

        public Categoria? ObtenerPorId(int id) => categorias.FirstOrDefault(c => c.Id == id);

        public void Agregar(Categoria categoria)
        {
            categoria.Id = categorias.Any() ? categorias.Max(c => c.Id) + 1 : 1;
            categorias.Add(categoria);
        }

        public void Actualizar(Categoria categoria)
        {
            var index = categorias.FindIndex(c => c.Id == categoria.Id);
            if (index != -1)
                categorias[index] = categoria;
        }

        public void Eliminar(int id)
        {
            var categoria = ObtenerPorId(id);
            if (categoria != null)
                categorias.Remove(categoria);
        }
    }
}
