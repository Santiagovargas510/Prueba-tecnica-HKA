using System.Data;

namespace SistemaVentas.Domain.Entities;

public class Factura
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } = null!;
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public decimal Total { get; set; }
    public string Estado { get; set; } = "Activa";
    public ICollection<DetalleFactura> Detalles { get; set; } = new List<DetalleFactura>();
}