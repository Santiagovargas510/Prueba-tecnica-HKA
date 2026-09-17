namespace SistemaVentas.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using SistemaVentas.Domain.Entities;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<Factura> Facturas => Set<Factura>();
    public DbSet<DetalleFactura> DetallesFactura => Set<DetalleFactura>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Cliente
        modelBuilder.Entity<Cliente>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Nombre).IsRequired().HasMaxLength(100);
            e.Property(c => c.Email).IsRequired().HasMaxLength(100);
            e.Property(c => c.Telefono).HasMaxLength(20);
        });

        // Producto
        modelBuilder.Entity<Producto>(e =>
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
            e.Property(p => p.Precio).HasColumnType("decimal(18,2)");
        });

        // Factura
        modelBuilder.Entity<Factura>(e =>
        {
            e.HasKey(f => f.Id);
            e.Property(f => f.Total).HasColumnType("decimal(18,2)");
            e.HasOne(f => f.Cliente)
             .WithMany(c => c.Facturas)
             .HasForeignKey(f => f.ClienteId);
        });

        // DetalleFactura
        modelBuilder.Entity<DetalleFactura>(e =>
        {
            e.HasKey(d => d.Id);
            e.Property(d => d.PrecioUnitario).HasColumnType("decimal(18,2)");
            e.Property(d => d.Subtotal).HasColumnType("decimal(18,2)");
            e.HasOne(d => d.Factura)
             .WithMany(f => f.Detalles)
             .HasForeignKey(d => d.FacturaId);
            e.HasOne(d => d.Producto)
             .WithMany(p => p.Detalles)
             .HasForeignKey(d => d.ProductoId);
        });

        // Datos semilla
        modelBuilder.Entity<Cliente>().HasData(
            new Cliente { Id = 1, Nombre = "Juan Pérez", Email = "juan@email.com", Telefono = "3001234567", Activo = true },
            new Cliente { Id = 2, Nombre = "María García", Email = "maria@email.com", Telefono = "3109876543", Activo = true },
            new Cliente { Id = 3, Nombre = "Carlos López", Email = "carlos@email.com", Telefono = "3204567890", Activo = true }
        );

        modelBuilder.Entity<Producto>().HasData(
            new Producto { Id = 1, Nombre = "Laptop", Descripcion = "Laptop 15 pulgadas", Precio = 2500000, Stock = 10, Activo = true },
            new Producto { Id = 2, Nombre = "Mouse", Descripcion = "Mouse inalámbrico", Precio = 85000, Stock = 50, Activo = true },
            new Producto { Id = 3, Nombre = "Teclado", Descripcion = "Teclado mecánico", Precio = 150000, Stock = 30, Activo = true },
            new Producto { Id = 4, Nombre = "Monitor", Descripcion = "Monitor 24 pulgadas", Precio = 900000, Stock = 15, Activo = true }
        );
    }
}