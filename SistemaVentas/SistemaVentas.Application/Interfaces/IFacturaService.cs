namespace SistemaVentas.Application.Interfaces;

using SistemaVentas.Application.DTOs;

public interface IFacturaService
{
    Task<IEnumerable<FacturaDto>> ObtenerTodasAsync();
    Task<FacturaDto?> ObtenerPorIdAsync(int id);
    Task<FacturaDto> CrearAsync(CrearFacturaDto dto);
}