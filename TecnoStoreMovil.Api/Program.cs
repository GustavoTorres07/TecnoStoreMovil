using Microsoft.AspNetCore.HttpOverrides; 
using Microsoft.EntityFrameworkCore;
using TecnoStoreMovil.Api.Data;
using TecnoStoreMovil.Api.Services.Contrato;
using TecnoStoreMovil.Api.Services.Implementacion;

var builder = WebApplication.CreateBuilder(args);

// EF Core
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("TecnoStoreDB")));

// CORS (para Postman/MAUI). Si luego usás cookies/sesión, cambiá a WithOrigins(...).AllowCredentials()
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("any", p => p
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ICategoriaAdminService, CategoriaAdminService>();
builder.Services.AddScoped<IProductoAdminService, ProductoAdminService>();




var app = builder.Build();

// 🔐 HSTS solo en producción (mejora seguridad y evita warnings)
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

// 👉 MUY IMPORTANTE EN HOSTING (Somee/IIS): respeta HTTPS del proxy
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedFor
});

// 🔁 Redirige todo a https (si entra por http)
app.UseHttpsRedirection();

app.UseCors("any");

app.UseAuthorization();

app.MapControllers();

// Health/simple ping
app.MapGet("/", () => Results.Ok(new { ok = true, api = "TecnoStoreMovil.Api" }));

app.Run();
