namespace SistemaVentas.Application.Services;

using SistemaVentas.Application.DTOs;
using SistemaVentas.Application.Interfaces;
using SistemaVentas.Domain.Entities;

public class FacturaService : IFacturaService
{
    private readonly IFacturaRepositorio _facturaRepositorio;
    private readonly IRepositorio<Cliente> _clienteRepositorio;
    private readonly IRepositorio<Producto> _productoRepositorio;

    public FacturaService(
        IFacturaRepositorio facturaRepositorio,
        IRepositorio<Cliente> clienteRepositorio,
        IRepositorio<Producto> productoRepositorio)
    {
        _facturaRepositorio = facturaRepositorio;
        _clienteRepositorio = clienteRepositorio;
        _productoRepositorio = productoRepositorio;
    }

    public async Task<IEnumerable<FacturaDto>> ObtenerTodasAsync()
    {
        var facturas = await _facturaRepositorio.ObtenerTodasConDetallesAsync();
        return facturas.Select(MapearFactura);
    }

    public async Task<FacturaDto?> ObtenerPorIdAsync(int id)
    {
        var factura = await _facturaRepositorio.ObtenerPorIdConDetallesAsync(id);
        return factura == null ? null : MapearFactura(factura);
    }

    public async Task<FacturaDto> CrearAsync(CrearFacturaDto dto)
    {
        if (dto.ClienteId <= 0)
            throw new ArgumentException("Debe seleccionar un cliente válido.");
        if (dto.Detalles == null || dto.Detalles.Count == 0)
            throw new ArgumentException("La factura debe tener al menos un producto.");

        var cliente = await _clienteRepositorio.ObtenerPorIdAsync(dto.ClienteId)
            ?? throw new ArgumentException("Cliente no encontrado.");

        var factura = new Factura
        {
            ClienteId = dto.ClienteId,
            Fecha = DateTime.UtcNow,
            Detalles = new List<DetalleFactura>()
        };

        decimal total = 0;

        foreach (var item in dto.Detalles)
        {
            var producto = await _productoRepositorio.ObtenerPorIdAsync(item.ProductoId)
                ?? throw new ArgumentException($"Producto {item.ProductoId} no encontrado.");

            if (item.Cantidad <= 0)
                throw new ArgumentException($"La cantidad de {producto.Nombre} debe ser mayor a cero.");
            if (producto.Stock < item.Cantidad)
                throw new ArgumentException($"Stock insuficiente para {producto.Nombre}. Disponible: {producto.Stock}");

            var subtotal = producto.Precio * item.Cantidad;
            total += subtotal;
            producto.Stock -= item.Cantidad;
            await _productoRepositorio.ActualizarAsync(producto);

            factura.Detalles.Add(new DetalleFactura
            {
                ProductoId = producto.Id,
                Cantidad = item.Cantidad,
                PrecioUnitario = producto.Precio,
                Subtotal = subtotal
            });
        }

        factura.Total = total;
        await _facturaRepositorio.AgregarAsync(factura);

        return await ObtenerPorIdAsync(factura.Id)
            ?? throw new Exception("Error al recuperar la factura creada.");
    }

    private static FacturaDto MapearFactura(Factura f) => new()
    {
        Id = f.Id,
        ClienteId = f.ClienteId,
        ClienteNombre = f.Cliente?.Nombre ?? "",
        Fecha = f.Fecha,
        Total = f.Total,
        Estado = f.Estado,
        Detalles = f.Detalles.Select(d => new DetalleFacturaDto
        {
            ProductoId = d.ProductoId,
            ProductoNombre = d.Producto?.Nombre ?? "",
            Cantidad = d.Cantidad,
            PrecioUnitario = d.PrecioUnitario,
            Subtotal = d.Subtotal
        }).ToList()
    };
}