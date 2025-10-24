using DataAccess;
using Domain;

namespace BusinessLogic;

public class PeliculaService : IPeliculaService
{
    private readonly IPeliculaRepository _repository;

    public PeliculaService(IPeliculaRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Pelicula>> ObtenerTodasAsync()
    {
        return await _repository.ObtenerTodasAsync();
    }

    public async Task<Pelicula?> ObtenerPorIdAsync(int id)
    {
        return await _repository.ObtenerPorIdAsync(id);
    }

    public async Task<Pelicula> CrearAsync(string titulo, string director, int anio, string genero, double calificacion)
    {
        var pelicula = new Pelicula(titulo, director, anio, genero, calificacion);
        return await _repository.CrearAsync(pelicula);
    }

    public async Task ActualizarAsync(int id, string titulo, string director, int anio, string genero, double calificacion)
    {
        var pelicula = await _repository.ObtenerPorIdAsync(id);
        if (pelicula == null)
            throw new InvalidOperationException($"No se encontró la película con ID {id}");

        pelicula.Actualizar(titulo, director, anio, genero, calificacion);
        await _repository.ActualizarAsync(pelicula);
    }

    public async Task CambiarEstadoVistaAsync(int id)
    {
        var pelicula = await _repository.ObtenerPorIdAsync(id);
        if (pelicula == null)
            throw new InvalidOperationException($"No se encontró la película con ID {id}");

        if (pelicula.Vista)
            pelicula.MarcarComoNoVista();
        else
            pelicula.MarcarComoVista();

        await _repository.ActualizarAsync(pelicula);
    }

    public async Task EliminarAsync(int id)
    {
        await _repository.EliminarAsync(id);
    }
}
