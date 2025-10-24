using Domain;

namespace DTOs;

public class PeliculaDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Director { get; set; } = string.Empty;
    public int Anio { get; set; }
    public string Genero { get; set; } = string.Empty;
    public bool Vista { get; set; }
    public double Calificacion { get; set; }

    public static PeliculaDto FromDomain(Pelicula pelicula)
    {
        return new PeliculaDto
        {
            Id = pelicula.Id,
            Titulo = pelicula.Titulo,
            Director = pelicula.Director,
            Anio = pelicula.Anio,
            Genero = pelicula.Genero,
            Vista = pelicula.Vista,
            Calificacion = pelicula.Calificacion
        };
    }
}
