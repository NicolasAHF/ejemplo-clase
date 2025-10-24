namespace Domain;

public class Pelicula
{
    public int Id { get; private set; }
    public string Titulo { get; private set; }
    public string Director { get; private set; }
    public int Anio { get; private set; }
    public string Genero { get; private set; }
    public bool Vista { get; private set; }
    public double Calificacion { get; private set; }

    // Constructor privado para EF Core
    private Pelicula()
    {
        Titulo = string.Empty;
        Director = string.Empty;
        Genero = string.Empty;
    }

    // Constructor para crear nueva película
    public Pelicula(string titulo, string director, int anio, string genero, double calificacion)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("El título no puede estar vacío", nameof(titulo));

        if (string.IsNullOrWhiteSpace(director))
            throw new ArgumentException("El director no puede estar vacío", nameof(director));

        if (string.IsNullOrWhiteSpace(genero))
            throw new ArgumentException("El género no puede estar vacío", nameof(genero));

        if (anio < 1888 || anio > DateTime.UtcNow.Year + 5)
            throw new ArgumentException("El año no es válido", nameof(anio));

        if (calificacion < 0 || calificacion > 10)
            throw new ArgumentException("La calificación debe estar entre 0 y 10", nameof(calificacion));

        Titulo = titulo;
        Director = director;
        Anio = anio;
        Genero = genero;
        Vista = false;
        Calificacion = calificacion;
    }

    // Método para marcar como vista
    public void MarcarComoVista()
    {
        Vista = true;
    }

    // Método para marcar como no vista
    public void MarcarComoNoVista()
    {
        Vista = false;
    }

    // Método para actualizar
    public void Actualizar(string titulo, string director, int anio, string genero, double calificacion)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("El título no puede estar vacío", nameof(titulo));

        if (string.IsNullOrWhiteSpace(director))
            throw new ArgumentException("El director no puede estar vacío", nameof(director));

        if (string.IsNullOrWhiteSpace(genero))
            throw new ArgumentException("El género no puede estar vacío", nameof(genero));

        if (anio < 1888 || anio > DateTime.UtcNow.Year + 5)
            throw new ArgumentException("El año no es válido", nameof(anio));

        if (calificacion < 0 || calificacion > 10)
            throw new ArgumentException("La calificación debe estar entre 0 y 10", nameof(calificacion));

        Titulo = titulo;
        Director = director;
        Anio = anio;
        Genero = genero;
        Calificacion = calificacion;
    }
}
