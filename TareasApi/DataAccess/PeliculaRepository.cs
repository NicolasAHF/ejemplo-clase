using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class PeliculaRepository : IPeliculaRepository
{
    private readonly PeliculaDbContext _context;

    public PeliculaRepository(PeliculaDbContext context)
    {
        _context = context;
    }

    public async Task<List<Pelicula>> ObtenerTodasAsync()
    {
        return await _context.Peliculas.ToListAsync();
    }

    public async Task<Pelicula?> ObtenerPorIdAsync(int id)
    {
        return await _context.Peliculas.FindAsync(id);
    }

    public async Task<Pelicula> CrearAsync(Pelicula pelicula)
    {
        _context.Peliculas.Add(pelicula);
        await _context.SaveChangesAsync();
        return pelicula;
    }

    public async Task ActualizarAsync(Pelicula pelicula)
    {
        _context.Peliculas.Update(pelicula);
        await _context.SaveChangesAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var pelicula = await ObtenerPorIdAsync(id);
        if (pelicula != null)
        {
            _context.Peliculas.Remove(pelicula);
            await _context.SaveChangesAsync();
        }
    }
}
