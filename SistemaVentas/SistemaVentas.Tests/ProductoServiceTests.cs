namespace SistemaVentas.Tests;

using Microsoft.EntityFrameworkCore;
using SistemaVentas.Application.DTOs;
using SistemaVentas.Application.Services;
using SistemaVentas.Infrastructure.Data;
using SistemaVentas.Infrastructure.Repositories;
using Xunit;

public class ProductoServiceTests
{
    private AppDbContext CrearContexto()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task CrearProducto_ConDatosValidos_RetornaProductoCreado()
    {
        // Arrange
        var context = CrearContexto();
        var repositorio = new RepositorioBase<SistemaVentas.Domain.Entities.Producto>(context);
        var service = new ProductoService(repositorio);
        var dto = new CrearProductoDto
        {
            Nombre = "Laptop",
            Descripcion = "Laptop 15 pulgadas",
            Precio = 2500000,
            Stock = 10
        };

        // Act
        var resultado = await service.CrearAsync(dto);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("Laptop", resultado.Nombre);
        Assert.Equal(2500000, resultado.Precio);
        Assert.Equal(10, resultado.Stock);
    }

    [Fact]
    public async Task CrearProducto_PrecioNegativo_LanzaExcepcion()
    {
        // Arrange
        var context = CrearContexto();
        var repositorio = new RepositorioBase<SistemaVentas.Domain.Entities.Producto>(context);
        var service = new ProductoService(repositorio);
        var dto = new CrearProductoDto { Nombre = "Test", Precio = -100, Stock = 10 };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => service.CrearAsync(dto));
    }

    [Fact]
    public async Task CrearProducto_StockNegativo_LanzaExcepcion()
    {
        // Arrange
        var context = CrearContexto();
        var repositorio = new RepositorioBase<SistemaVentas.Domain.Entities.Producto>(context);
        var service = new ProductoService(repositorio);
        var dto = new CrearProductoDto { Nombre = "Test", Precio = 100, Stock = -5 };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => service.CrearAsync(dto));
    }

    [Fact]
    public async Task CrearProducto_SinNombre_LanzaExcepcion()
    {
        // Arrange
        var context = CrearContexto();
        var repositorio = new RepositorioBase<SistemaVentas.Domain.Entities.Producto>(context);
        var service = new ProductoService(repositorio);
        var dto = new CrearProductoDto { Nombre = "", Precio = 100, Stock = 10 };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => service.CrearAsync(dto));
    }
}