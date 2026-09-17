namespace SistemaVentas.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using SistemaVentas.Application.Interfaces;
using SistemaVentas.Infrastructure.Data;

public class RepositorioBase<T> : IRepositorio<T> where T : class
{
    protected readonly AppDbContext _context;

    public RepositorioBase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> ObtenerTodosAsync()
        => await _context.Set<T>().ToListAsync();

    public async Task<T?> ObtenerPorIdAsync(int id)
        => await _context.Set<T>().FindAsync(id);

    public async Task AgregarAsync(T entidad)
    {
        await _context.Set<T>().AddAsync(entidad);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(T entidad)
    {
        _context.Set<T>().Update(entidad);
        await _context.SaveChangesAsync();
    }
}