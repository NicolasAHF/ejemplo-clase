namespace DTOs;

public class CrearPeliculaDto
{
    public string Titulo { get; set; } = string.Empty;
    public string Director { get; set; } = string.Empty;
    public int Anio { get; set; }
    public string Genero { get; set; } = string.Empty;
    public double Calificacion { get; set; }
}
