namespace SistemaVentas.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using SistemaVentas.Application.DTOs;
using SistemaVentas.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly IProductoService _service;

    public ProductosController(IProductoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var productos = await _service.ObtenerTodosAsync();
        return Ok(productos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var producto = await _service.ObtenerPorIdAsync(id);
        if (producto == null) return NotFound(new { mensaje = "Producto no encontrado." });
        return Ok(producto);
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearProductoDto dto)
    {
        try
        {
            var producto = await _service.CrearAsync(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = producto.Id }, producto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var resultado = await _service.EliminarAsync(id);
        if (!resultado) return NotFound(new { mensaje = "Producto no encontrado." });
        return Ok(new { mensaje = "Producto eliminado correctamente." });
    }
}