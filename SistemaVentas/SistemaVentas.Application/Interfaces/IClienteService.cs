namespace SistemaVentas.Application.Interfaces;

using SistemaVentas.Application.DTOs;

public interface IClienteService
{
    Task<IEnumerable<ClienteDto>> ObtenerTodosAsync();
    Task<ClienteDto?> ObtenerPorIdAsync(int id);
    Task<ClienteDto> CrearAsync(CrearClienteDto dto);
    Task<bool> EliminarAsync(int id);
}