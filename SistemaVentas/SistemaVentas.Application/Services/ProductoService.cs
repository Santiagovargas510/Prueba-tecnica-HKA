namespace SistemaVentas.Application.Services;

using SistemaVentas.Application.DTOs;
using SistemaVentas.Application.Interfaces;
using SistemaVentas.Domain.Entities;

public class ProductoService : IProductoService
{
    private readonly IRepositorio<Producto> _repositorio;

    public ProductoService(IRepositorio<Producto> repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<IEnumerable<ProductoDto>> ObtenerTodosAsync()
    {
        var productos = await _repositorio.ObtenerTodosAsync();
        return productos.Where(p => p.Activo).Select(p => new ProductoDto
        {
            Id = p.Id,
            Nombre = p.Nombre,
            Descripcion = p.Descripcion,
            Precio = p.Precio,
            Stock = p.Stock
        });
    }

    public async Task<ProductoDto?> ObtenerPorIdAsync(int id)
    {
        var producto = await _repositorio.ObtenerPorIdAsync(id);
        if (producto == null || !producto.Activo) return null;
        return new ProductoDto
        {
            Id = producto.Id,
            Nombre = producto.Nombre,
            Descripcion = producto.Descripcion,
            Precio = producto.Precio,
            Stock = producto.Stock
        };
    }

    public async Task<ProductoDto> CrearAsync(CrearProductoDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Nombre))
            throw new ArgumentException("El nombre es obligatorio.");
        if (dto.Precio <= 0)
            throw new ArgumentException("El precio debe ser mayor a cero.");
        if (dto.Stock < 0)
            throw new ArgumentException("El stock no puede ser negativo.");

        var producto = new Producto
        {
            Nombre = dto.Nombre.Trim(),
            Descripcion = dto.Descripcion.Trim(),
            Precio = dto.Precio,
            Stock = dto.Stock
        };

        await _repositorio.AgregarAsync(producto);
        return new ProductoDto
        {
            Id = producto.Id,
            Nombre = producto.Nombre,
            Descripcion = producto.Descripcion,
            Precio = producto.Precio,
            Stock = producto.Stock
        };
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var producto = await _repositorio.ObtenerPorIdAsync(id);
        if (producto == null) return false;
        producto.Activo = false;
        await _repositorio.ActualizarAsync(producto);
        return true;
    }
}