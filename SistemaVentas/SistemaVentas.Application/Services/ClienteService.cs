namespace SistemaVentas.Application.Services;

using SistemaVentas.Application.DTOs;
using SistemaVentas.Application.Interfaces;
using SistemaVentas.Domain.Entities;

public class ClienteService : IClienteService
{
    private readonly IRepositorio<Cliente> _repositorio;

    public ClienteService(IRepositorio<Cliente> repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<IEnumerable<ClienteDto>> ObtenerTodosAsync()
    {
        var clientes = await _repositorio.ObtenerTodosAsync();
        return clientes.Where(c => c.Activo).Select(c => new ClienteDto
        {
            Id = c.Id,
            Nombre = c.Nombre,
            Email = c.Email,
            Telefono = c.Telefono
        });
    }

    public async Task<ClienteDto?> ObtenerPorIdAsync(int id)
    {
        var cliente = await _repositorio.ObtenerPorIdAsync(id);
        if (cliente == null || !cliente.Activo) return null;
        return new ClienteDto
        {
            Id = cliente.Id,
            Nombre = cliente.Nombre,
            Email = cliente.Email,
            Telefono = cliente.Telefono
        };
    }

    public async Task<ClienteDto> CrearAsync(CrearClienteDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Nombre))
            throw new ArgumentException("El nombre es obligatorio.");
        if (string.IsNullOrWhiteSpace(dto.Email))
            throw new ArgumentException("El email es obligatorio.");

        var cliente = new Cliente
        {
            Nombre = dto.Nombre.Trim(),
            Email = dto.Email.Trim(),
            Telefono = dto.Telefono.Trim()
        };

        await _repositorio.AgregarAsync(cliente);
        return new ClienteDto
        {
            Id = cliente.Id,
            Nombre = cliente.Nombre,
            Email = cliente.Email,
            Telefono = cliente.Telefono
        };
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var cliente = await _repositorio.ObtenerPorIdAsync(id);
        if (cliente == null) return false;
        cliente.Activo = false;
        await _repositorio.ActualizarAsync(cliente);
        return true;
    }
}