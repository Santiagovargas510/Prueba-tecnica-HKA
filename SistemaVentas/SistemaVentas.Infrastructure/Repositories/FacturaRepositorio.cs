namespace SistemaVentas.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using SistemaVentas.Application.Interfaces;
using SistemaVentas.Domain.Entities;
using SistemaVentas.Infrastructure.Data;

public class FacturaRepositorio : RepositorioBase<Factura>, IFacturaRepositorio
{
    public FacturaRepositorio(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Factura>> ObtenerTodasConDetallesAsync()
        => await _context.Facturas
            .Include(f => f.Cliente)
            .Include(f => f.Detalles)
                .ThenInclude(d => d.Producto)
            .ToListAsync();

    public async Task<Factura?> ObtenerPorIdConDetallesAsync(int id)
        => await _context.Facturas
            .Include(f => f.Cliente)
            .Include(f => f.Detalles)
                .ThenInclude(d => d.Producto)
            .FirstOrDefaultAsync(f => f.Id == id);
}