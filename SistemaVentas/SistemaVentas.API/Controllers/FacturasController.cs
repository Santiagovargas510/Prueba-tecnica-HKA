namespace SistemaVentas.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using SistemaVentas.Application.DTOs;
using SistemaVentas.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class FacturasController : ControllerBase
{
    private readonly IFacturaService _service;

    public FacturasController(IFacturaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodas()
    {
        var facturas = await _service.ObtenerTodasAsync();
        return Ok(facturas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var factura = await _service.ObtenerPorIdAsync(id);
        if (factura == null) return NotFound(new { mensaje = "Factura no encontrada." });
        return Ok(factura);
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearFacturaDto dto)
    {
        try
        {
            var factura = await _service.CrearAsync(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = factura.Id }, factura);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }
}