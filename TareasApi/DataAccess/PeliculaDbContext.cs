using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class PeliculaDbContext : DbContext
{
    public PeliculaDbContext(DbContextOptions<PeliculaDbContext> options) : base(options)
    {
    }

    public DbSet<Pelicula> Peliculas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de la entidad Pelicula
        modelBuilder.Entity<Pelicula>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Titulo).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Director).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Anio).IsRequired();
            entity.Property(p => p.Genero).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Vista).IsRequired();
            entity.Property(p => p.Calificacion).IsRequired();
        });

        // Datos de prueba (seed)
        modelBuilder.Entity<Pelicula>().HasData(
            new
            {
                Id = 1,
                Titulo = "El Padrino",
                Director = "Francis Ford Coppola",
                Anio = 1972,
                Genero = "Drama",
                Vista = false,
                Calificacion = 9.2
            },
            new
            {
                Id = 2,
                Titulo = "Pulp Fiction",
                Director = "Quentin Tarantino",
                Anio = 1994,
                Genero = "Crimen",
                Vista = true,
                Calificacion = 8.9
            },
            new
            {
                Id = 3,
                Titulo = "Inception",
                Director = "Christopher Nolan",
                Anio = 2010,
                Genero = "Ciencia Ficción",
                Vista = false,
                Calificacion = 8.8
            }
        );
    }
}
