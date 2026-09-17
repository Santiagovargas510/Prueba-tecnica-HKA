namespace SistemaVentas.Application.Interfaces;

using SistemaVentas.Domain.Entities;

public interface IFacturaRepositorio : IRepositorio<Factura>
{
    Task<IEnumerable<Factura>> ObtenerTodasConDetallesAsync();
    Task<Factura?> ObtenerPorIdConDetallesAsync(int id);
}