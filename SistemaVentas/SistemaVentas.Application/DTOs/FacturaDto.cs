namespace SistemaVentas.Application.DTOs;

public class FacturaDto
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public string ClienteNombre { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
    public decimal Total { get; set; }
    public string Estado { get; set; } = string.Empty;
    public List<DetalleFacturaDto> Detalles { get; set; } = new();
}

public class CrearFacturaDto
{
    public int ClienteId { get; set; }
    public List<CrearDetalleDto> Detalles { get; set; } = new();
}

public class CrearDetalleDto
{
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
}

public class DetalleFacturaDto
{
    public int ProductoId { get; set; }
    public string ProductoNombre { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; }
}