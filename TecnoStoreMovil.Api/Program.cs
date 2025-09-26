using Microsoft.AspNetCore.HttpOverrides; 
using Microsoft.EntityFrameworkCore;
using TecnoStoreMovil.Api.Data;
using TecnoStoreMovil.Api.Services.Contrato;
using TecnoStoreMovil.Api.Services.Implementa;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("TecnoStoreDB")));

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
builder.Services.AddScoped<ICarritoService, CarritoService>();
builder.Services.AddScoped<IPedidosService, PedidosService>();
builder.Services.AddScoped<IAdminPedidosService, AdminPedidosService>();
builder.Services.AddScoped<ICarritoService, CarritoService>();
builder.Services.AddScoped<IPedidosService, PedidosService>();
builder.Services.AddScoped<IAdminPedidosService, AdminPedidosService>();




var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedFor
});

app.UseHttpsRedirection();

app.UseCors("any");

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => Results.Ok(new { ok = true, api = "TecnoStoreMovil.Api" }));

app.Run();
