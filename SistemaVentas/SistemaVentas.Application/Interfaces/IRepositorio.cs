namespace SistemaVentas.Application.Interfaces;

public interface IRepositorio<T> where T : class
{
    Task<IEnumerable<T>> ObtenerTodosAsync();
    Task<T?> ObtenerPorIdAsync(int id);
    Task AgregarAsync(T entidad);
    Task ActualizarAsync(T entidad);
}