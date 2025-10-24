using Domain;

namespace BusinessLogic;

public interface IPeliculaService
{
    Task<List<Pelicula>> ObtenerTodasAsync();
    Task<Pelicula?> ObtenerPorIdAsync(int id);
    Task<Pelicula> CrearAsync(string titulo, string director, int anio, string genero, double calificacion);
    Task ActualizarAsync(int id, string titulo, string director, int anio, string genero, double calificacion);
    Task CambiarEstadoVistaAsync(int id);
    Task EliminarAsync(int id);
}
