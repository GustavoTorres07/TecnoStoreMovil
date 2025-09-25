using Microsoft.EntityFrameworkCore;
using TecnoStoreMovil.Shared.Models;

namespace TecnoStoreMovil.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }

        public DbSet<Rol> Roles => Set<Rol>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Direccion> Direcciones => Set<Direccion>();
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos => Set<Producto>();
        public DbSet<Pedido> Pedidos => Set<Pedido>();
        public DbSet<PedidoItem> PedidoItems => Set<PedidoItem>();
        public DbSet<UsuarioRol> UsuarioRoles => Set<UsuarioRol>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            // ===== rol =====
            b.Entity<Rol>().ToTable("rol", "dbo");
            b.Entity<Rol>().Property(x => x.Id).HasColumnName("id");
            b.Entity<Rol>().Property(x => x.Nombre).HasColumnName("nombre").IsRequired();
            b.Entity<Rol>().Property(x => x.Descripcion).HasColumnName("descripcion");
            b.Entity<Rol>().Property(x => x.Activo).HasColumnName("activo");
            b.Entity<Rol>().HasMany(x => x.UsuarioRoles).WithOne(x => x.Rol).HasForeignKey(x => x.RolId);

            // ===== usuario =====
            b.Entity<Usuario>().ToTable("usuario", "dbo");
            b.Entity<Usuario>().Property(x => x.Id).HasColumnName("id");
            b.Entity<Usuario>().Property(x => x.Nombre).HasColumnName("nombre").IsRequired();
            b.Entity<Usuario>().Property(x => x.Apellido).HasColumnName("apellido").IsRequired();
            b.Entity<Usuario>().Property(x => x.Email).HasColumnName("email").IsRequired();
            b.Entity<Usuario>().Property(x => x.Clave).HasColumnName("clave").IsRequired();
            b.Entity<Usuario>().Property(x => x.Telefono).HasColumnName("telefono");
            b.Entity<Usuario>().Property(x => x.FechaAlta).HasColumnName("fecha_alta");
            b.Entity<Usuario>().Property(x => x.Activo).HasColumnName("activo");
            b.Entity<Usuario>().HasIndex(x => x.Email).IsUnique(); // UQ email (ya existe en BD)

            // nav 1:1 dirección
            b.Entity<Usuario>()
                .HasOne(u => u.Direccion)
                .WithOne(d => d.Usuario)
                .HasForeignKey<Direccion>(d => d.UsuarioId);

            // ===== direccion (1:1 con usuario, UNIQUE usuario_id) =====
            b.Entity<Direccion>().ToTable("direccion", "dbo");
            b.Entity<Direccion>().Property(x => x.Id).HasColumnName("id");
            b.Entity<Direccion>().Property(x => x.UsuarioId).HasColumnName("usuario_id");
            b.Entity<Direccion>().Property(x => x.Calle).HasColumnName("calle").IsRequired();
            b.Entity<Direccion>().Property(x => x.Numero).HasColumnName("numero");
            b.Entity<Direccion>().Property(x => x.Ciudad).HasColumnName("ciudad").IsRequired();
            b.Entity<Direccion>().Property(x => x.Provincia).HasColumnName("provincia").IsRequired();
            b.Entity<Direccion>().Property(x => x.CodigoPostal).HasColumnName("codigo_postal").IsRequired();
            b.Entity<Direccion>().Property(x => x.Pais).HasColumnName("pais").IsRequired();
            b.Entity<Direccion>().HasIndex(x => x.UsuarioId).IsUnique(); // garantiza 1 dirección por usuario

            // ===== usuario_rol (PK compuesta) =====
            b.Entity<UsuarioRol>().ToTable("usuario_rol", "dbo");
            b.Entity<UsuarioRol>().HasKey(x => new { x.UsuarioId, x.RolId });
            b.Entity<UsuarioRol>().Property(x => x.UsuarioId).HasColumnName("usuario_id");
            b.Entity<UsuarioRol>().Property(x => x.RolId).HasColumnName("rol_id");
            b.Entity<UsuarioRol>()
                .HasOne(ur => ur.Usuario).WithMany(u => u.UsuarioRoles).HasForeignKey(ur => ur.UsuarioId);
            b.Entity<UsuarioRol>()
                .HasOne(ur => ur.Rol).WithMany(r => r.UsuarioRoles).HasForeignKey(ur => ur.RolId);

            // ===== categoria =====
            b.Entity<Categoria>().ToTable("categoria", "dbo");
            b.Entity<Categoria>().Property(x => x.Id).HasColumnName("id");
            b.Entity<Categoria>().Property(x => x.Nombre).HasColumnName("nombre").IsRequired();
            b.Entity<Categoria>().Property(x => x.Descripcion).HasColumnName("descripcion");
            b.Entity<Categoria>().Property(x => x.Activo).HasColumnName("activo");
            b.Entity<Categoria>().HasIndex(x => x.Nombre).IsUnique(); // UQ nombre

            // ===== producto =====
            b.Entity<Producto>().ToTable("producto", "dbo");
            b.Entity<Producto>().Property(x => x.Id).HasColumnName("id");
            b.Entity<Producto>().Property(x => x.CategoriaId).HasColumnName("categoria_id");
            b.Entity<Producto>().Property(x => x.Nombre).HasColumnName("nombre").IsRequired();
            b.Entity<Producto>().Property(x => x.Descripcion).HasColumnName("descripcion");
            b.Entity<Producto>().Property(x => x.Precio).HasColumnName("precio");
            b.Entity<Producto>().Property(x => x.Stock).HasColumnName("stock");
            b.Entity<Producto>().Property(x => x.ImagenUrl).HasColumnName("imagen_url");
            b.Entity<Producto>().Property(x => x.Activo).HasColumnName("activo");
            b.Entity<Producto>()
                .HasOne(p => p.Categoria).WithMany(c => c.Productos).HasForeignKey(p => p.CategoriaId);
            b.Entity<Producto>().HasIndex(p => p.CategoriaId);
            b.Entity<Producto>().HasIndex(p => p.Nombre).IsUnique(); // UQ nombre

            // ===== pedido (cabecera) =====
            b.Entity<Pedido>().ToTable("pedido", "dbo");
            b.Entity<Pedido>().Property(x => x.Id).HasColumnName("id");
            b.Entity<Pedido>().Property(x => x.UsuarioId).HasColumnName("usuario_id");
            b.Entity<Pedido>().Property(x => x.FechaCreacion).HasColumnName("fecha_creacion");
            b.Entity<Pedido>().Property(x => x.Estado).HasColumnName("estado");
            b.Entity<Pedido>().Property(x => x.Total).HasColumnName("total");
            b.Entity<Pedido>()
                .HasOne(p => p.Usuario).WithMany(u => u.Pedidos).HasForeignKey(p => p.UsuarioId);
            b.Entity<Pedido>().HasIndex(p => p.UsuarioId);

            // ===== pedido_item (detalle) =====
            b.Entity<PedidoItem>().ToTable("pedido_item", "dbo");
            b.Entity<PedidoItem>().Property(x => x.Id).HasColumnName("id");
            b.Entity<PedidoItem>().Property(x => x.PedidoId).HasColumnName("pedido_id");
            b.Entity<PedidoItem>().Property(x => x.ProductoId).HasColumnName("producto_id");
            b.Entity<PedidoItem>().Property(x => x.Cantidad).HasColumnName("cantidad");
            b.Entity<PedidoItem>().Property(x => x.PrecioUnit).HasColumnName("precio_unit");
            // 'Subtotal' es calculado en C# (o podés proyectarlo en el SELECT)
            b.Entity<PedidoItem>()
                .HasOne(i => i.Pedido).WithMany(p => p.Items).HasForeignKey(i => i.PedidoId);
            b.Entity<PedidoItem>()
                .HasOne(i => i.Producto).WithMany(p => p.PedidoItems).HasForeignKey(i => i.ProductoId);
            b.Entity<PedidoItem>().HasIndex(i => i.PedidoId);
            b.Entity<PedidoItem>().HasIndex(i => i.ProductoId);
            b.Entity<PedidoItem>().HasIndex(i => new { i.PedidoId, i.ProductoId }).IsUnique(); // UQ(pedido_id, producto_id)
        }
    }
}
