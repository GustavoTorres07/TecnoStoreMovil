using System;
using System.Collections.Generic;
using System.Linq;
using TecnoStoreMovil.Models;

namespace TecnoStoreMovil.Services
{
    public class ProductoService
    {
        private readonly List<Producto> productos;
        private readonly List<Categoria> categorias;

        public ProductoService()
        {
            categorias = new List<Categoria>
            {
                new Categoria { Id = 1, Nombre = "Consolas", Descripcion = "Videoconsolas", Activo = true },
                new Categoria { Id = 2, Nombre = "Accesorios", Descripcion = "Periféricos y accesorios", Activo = true },
                new Categoria { Id = 3, Nombre = "Juegos", Descripcion = "Juegos por plataforma", Activo = true },
            };

            productos = new List<Producto>
            {
                new Producto {
                    Id = 1,
                    Nombre = "PlayStation 5",
                    Descripcion = "Consola Sony PS5 con lector de discos.",
                    Precio = 799.99m,
                    ImagenProducto = "images/ps5.jpg",
                    Cantidad = 10,
                    Stock = true,
                    CategoriaId = 1,
                    Categoria = categorias.First(c => c.Id == 1)
                },
                new Producto {
                    Id = 2,
                    Nombre = "Joystick DualSense",
                    Descripcion = "Control inalámbrico original para PS5.",
                    Precio = 99.99m,
                    ImagenProducto = "images/jyps5.jpg",
                    Cantidad = 25,
                    Stock = true,
                    CategoriaId = 2,
                    Categoria = categorias.First(c => c.Id == 2)
                },
                new Producto {
                    Id = 3,
                    Nombre = "Spider-Man: Miles Morales",
                    Descripcion = "Juego exclusivo de PS5",
                    Precio = 69.99m,
                    ImagenProducto = "images/spidermanps5.jpeg",
                    Cantidad = 15,
                    Stock = true,
                    CategoriaId = 3, // Juegos
                    Categoria = categorias.First(c => c.Id == 3),
                    Plataforma = PlataformaJuego.PS5,
                    FechaLanzamiento = new DateTime(2020, 11, 12)
                }
            };
        }

        public List<Producto> GetProductos() => productos;

        public Producto? GetProducto(int id) => productos.FirstOrDefault(p => p.Id == id);

        public void AddProducto(Producto producto)
        {
            producto.Id = productos.Any() ? productos.Max(p => p.Id) + 1 : 1;
            producto.Categoria = categorias.FirstOrDefault(c => c.Id == producto.CategoriaId);
            productos.Add(producto);
        }

        public void UpdateProducto(Producto producto)
        {
            var index = productos.FindIndex(p => p.Id == producto.Id);
            if (index != -1)
            {
                producto.Categoria = categorias.FirstOrDefault(c => c.Id == producto.CategoriaId);
                productos[index] = producto;
            }
        }

        public void DeleteProducto(int id)
        {
            var producto = GetProducto(id);
            if (producto != null)
                productos.Remove(producto);
        }

        public List<Categoria> GetCategorias() => categorias;

        public Categoria? GetCategoria(int id) => categorias.FirstOrDefault(c => c.Id == id);
    }
}
