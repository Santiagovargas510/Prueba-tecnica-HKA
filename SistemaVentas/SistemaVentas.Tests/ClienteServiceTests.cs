namespace SistemaVentas.Tests;

using Microsoft.EntityFrameworkCore;
using SistemaVentas.Application.DTOs;
using SistemaVentas.Application.Services;
using SistemaVentas.Infrastructure.Data;
using SistemaVentas.Infrastructure.Repositories;
using Xunit;

public class ClienteServiceTests
{
    private AppDbContext CrearContexto()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task CrearCliente_ConDatosValidos_RetornaClienteCreado()
    {
        // Arrange
        var context = CrearContexto();
        var repositorio = new RepositorioBase<SistemaVentas.Domain.Entities.Cliente>(context);
        var service = new ClienteService(repositorio);
        var dto = new CrearClienteDto
        {
            Nombre = "Juan Pérez",
            Email = "juan@email.com",
            Telefono = "3001234567"
        };

        // Act
        var resultado = await service.CrearAsync(dto);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("Juan Pérez", resultado.Nombre);
        Assert.Equal("juan@email.com", resultado.Email);
    }

    [Fact]
    public async Task CrearCliente_SinNombre_LanzaExcepcion()
    {
        // Arrange
        var context = CrearContexto();
        var repositorio = new RepositorioBase<SistemaVentas.Domain.Entities.Cliente>(context);
        var service = new ClienteService(repositorio);
        var dto = new CrearClienteDto { Nombre = "", Email = "juan@email.com" };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => service.CrearAsync(dto));
    }

    [Fact]
    public async Task CrearCliente_SinEmail_LanzaExcepcion()
    {
        // Arrange
        var context = CrearContexto();
        var repositorio = new RepositorioBase<SistemaVentas.Domain.Entities.Cliente>(context);
        var service = new ClienteService(repositorio);
        var dto = new CrearClienteDto { Nombre = "Juan", Email = "" };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => service.CrearAsync(dto));
    }

    [Fact]
    public async Task EliminarCliente_ClienteExistente_RetornaTrue()
    {
        // Arrange
        var context = CrearContexto();
        var repositorio = new RepositorioBase<SistemaVentas.Domain.Entities.Cliente>(context);
        var service = new ClienteService(repositorio);
        var dto = new CrearClienteDto { Nombre = "Test", Email = "test@email.com", Telefono = "123" };
        var cliente = await service.CrearAsync(dto);

        // Act
        var resultado = await service.EliminarAsync(cliente.Id);

        // Assert
        Assert.True(resultado);
    }

    [Fact]
    public async Task EliminarCliente_ClienteInexistente_RetornaFalse()
    {
        // Arrange
        var context = CrearContexto();
        var repositorio = new RepositorioBase<SistemaVentas.Domain.Entities.Cliente>(context);
        var service = new ClienteService(repositorio);

        // Act
        var resultado = await service.EliminarAsync(999);

        // Assert
        Assert.False(resultado);
    }
}