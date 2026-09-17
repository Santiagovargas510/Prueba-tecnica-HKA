namespace SistemaVentas.Application.Interfaces;

using SistemaVentas.Application.DTOs;

public interface IProductoService
{
    Task<IEnumerable<ProductoDto>> ObtenerTodosAsync();
    Task<ProductoDto?> ObtenerPorIdAsync(int id);
    Task<ProductoDto> CrearAsync(CrearProductoDto dto);
    Task<bool> EliminarAsync(int id);
}