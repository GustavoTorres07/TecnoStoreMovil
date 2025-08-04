using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TecnoStoreMovil.Models;

namespace TecnoStoreMovil.Services
{
    public class UsuarioService
    {
        private readonly List<Usuario> usuarios;

        public UsuarioService()
        {
            usuarios = new List<Usuario>()
            {
                new Usuario { Id = 1,
                    Nombre = "Gustavo",
                    Apellido = "Torres",
                    Dni = "39054025",
                    Email = "gtreal62@gmail.com",
                    Celular = "+54 9 2954-216751",
                    Clave = "admin123",
                    FechaRegistro = DateTime.Now,
                    FotoUsuario = "images/usuario_default.png",
                    Rol = RolUsuario.Administrador,
                    Activo = true

                },
                new Usuario { Id = 2,
                    Nombre = "Ana",
                    Apellido = "Perez",
                    Dni = "24584123",
                    Email = "anaperez@gmail.com",
                    Celular = "+54 9 2954-159634",
                    Clave = "anaperez1",
                    FechaRegistro = DateTime.Now,
                    FotoUsuario = "images/usuario_default.png",
                    Rol = RolUsuario.Cliente,   
                    Activo = true
                },
                new Usuario { Id = 3,
                    Nombre = "Luis",
                    Apellido = "Garcia",
                    Dni = "45519849",
                    Email = "anaperez@gmail.com",
                    Celular = "+54 9 2954-202541",
                    Clave = "anaperez1", 
                    FechaRegistro = DateTime.Now,
                    FotoUsuario = "images/usuario_default.png",
                    Rol = RolUsuario.Cliente,   
                    Activo = false
                }
            };
        }

        public List<Usuario> GetUsuarios() => usuarios;

        public Usuario? GetUsuario(int Id) => usuarios.FirstOrDefault(u => u.Id == Id);

        public Usuario? GetUsuarioPorEmail(string email) => usuarios.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

        public void AddUsuario(Usuario usuario)
        {
            usuario.Id = usuarios.Any() ? usuarios.Max(u => u.Id) + 1 : 1;
            usuarios.Add(usuario);
        }

        public void UpdateUsuario(Usuario usuario)
        {
            var index = usuarios.FindIndex(u => u.Id == usuario.Id);
            if (index >= 0) usuarios[index] = usuario;
        }
        public void DeleteUsuario(int id)
        {
            var usuario = GetUsuario(id);
            if (usuario != null) usuarios.Remove(usuario);
        }
        public Usuario? ValidarLogin(string email, string clave)
        {
            var user = GetUsuarioPorEmail(email);
            if (user != null && user.Clave == clave && user.Activo)
                return user;
            return null;
        }
    }
}
