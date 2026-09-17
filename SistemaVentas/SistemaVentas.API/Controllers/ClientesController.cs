namespace SistemaVentas.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using SistemaVentas.Application.DTOs;
using SistemaVentas.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _service;

    public ClientesController(IClienteService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var clientes = await _service.ObtenerTodosAsync();
        return Ok(clientes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var cliente = await _service.ObtenerPorIdAsync(id);
        if (cliente == null) return NotFound(new { mensaje = "Cliente no encontrado." });
        return Ok(cliente);
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearClienteDto dto)
    {
        try
        {
            var cliente = await _service.CrearAsync(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = cliente.Id }, cliente);
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
        if (!resultado) return NotFound(new { mensaje = "Cliente no encontrado." });
        return Ok(new { mensaje = "Cliente eliminado correctamente." });
    }
}