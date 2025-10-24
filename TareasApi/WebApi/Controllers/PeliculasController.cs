using BusinessLogic;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeliculasController : ControllerBase
{
    private readonly IPeliculaService _peliculaService;

    public PeliculasController(IPeliculaService peliculaService)
    {
        _peliculaService = peliculaService;
    }

    // GET: api/peliculas
    [HttpGet]
    public async Task<ActionResult<List<PeliculaDto>>> ObtenerTodas()
    {
        var peliculas = await _peliculaService.ObtenerTodasAsync();
        var peliculasDto = peliculas.Select(PeliculaDto.FromDomain).ToList();
        return Ok(peliculasDto);
    }

    // GET: api/peliculas/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PeliculaDto>> ObtenerPorId(int id)
    {
        var pelicula = await _peliculaService.ObtenerPorIdAsync(id);
        if (pelicula == null)
            return NotFound(new { mensaje = $"No se encontró la película con ID {id}" });

        return Ok(PeliculaDto.FromDomain(pelicula));
    }

    // POST: api/peliculas
    [HttpPost]
    public async Task<ActionResult<PeliculaDto>> Crear([FromBody] CrearPeliculaDto dto)
    {
        try
        {
            var pelicula = await _peliculaService.CrearAsync(dto.Titulo, dto.Director, dto.Anio, dto.Genero, dto.Calificacion);
            var peliculaDto = PeliculaDto.FromDomain(pelicula);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = pelicula.Id }, peliculaDto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    // PUT: api/peliculas/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarPeliculaDto dto)
    {
        try
        {
            await _peliculaService.ActualizarAsync(id, dto.Titulo, dto.Director, dto.Anio, dto.Genero, dto.Calificacion);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    // PATCH: api/peliculas/5/cambiar-estado-vista
    [HttpPatch("{id}/cambiar-estado-vista")]
    public async Task<ActionResult> CambiarEstadoVista(int id)
    {
        try
        {
            await _peliculaService.CambiarEstadoVistaAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
    }

    // DELETE: api/peliculas/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Eliminar(int id)
    {
        await _peliculaService.EliminarAsync(id);
        return NoContent();
    }
}
