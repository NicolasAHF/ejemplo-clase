using Domain;

namespace DataAccess;

public interface IPeliculaRepository
{
    Task<List<Pelicula>> ObtenerTodasAsync();
    Task<Pelicula?> ObtenerPorIdAsync(int id);
    Task<Pelicula> CrearAsync(Pelicula pelicula);
    Task ActualizarAsync(Pelicula pelicula);
    Task EliminarAsync(int id);
}
