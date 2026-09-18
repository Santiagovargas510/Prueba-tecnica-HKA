using Microsoft.EntityFrameworkCore;
using SistemaVentas.Application.Interfaces;
using SistemaVentas.Application.Services;
using SistemaVentas.Infrastructure.Data;
using SistemaVentas.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Base de datos SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=sistemaventas.db"));

// Repositorios
builder.Services.AddScoped(typeof(IRepositorio<>), typeof(RepositorioBase<>));
builder.Services.AddScoped<IFacturaRepositorio, FacturaRepositorio>();

// Servicios
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IFacturaService, FacturaService>();

// CORS para el frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Crear y migrar base de datos automáticamente
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowAll");
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();

app.Run();