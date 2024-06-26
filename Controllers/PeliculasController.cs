using System.Data.Common;
using backendnet.Data;
using backendnet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendnet.Controllers; 

[Route("api/[controller]")]
[ApiController]
public class PeliculasController : Controller
{
    private readonly IdentityContext _context;

    public PeliculasController(IdentityContext context)
    {
        _context = context;
    }
    //ge: api/peliculas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pelicula>>> GetPeliculas()
    {
        return await _context.Pelicula.Include(i => i.Categorias).AsNoTracking().ToListAsync();

    }
    //get: api/peliculas/5
    [HttpGet("{id}")]
    [Authorize(Roles = "Usuaio,Administrador")]
    public async Task<ActionResult<Pelicula>> GetPelicula(int id)
    {
        var pelicula = await _context.Pelicula.Include(i => i.Categorias).AsNoTracking().FirstOrDefaultAsync(s => s.PeliculaId ==id);
        if (pelicula == null)
        {
            return NotFound();
        }
        return pelicula;
    }
    //post: api/peliculas
    [HttpPost]
    [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<Pelicula>> PostPelicula(PeliculaDTO peliculaDTO)
    {

        Pelicula pelicula = new()
        { 
            Titulo = peliculaDTO.Titulo,
            Sinopsis =peliculaDTO.Sinopsis,
            Anio = peliculaDTO.Anio,
            Poster = peliculaDTO.Poster,
            Categorias = []
        };

        if (peliculaDTO.Categorias != null)
        {
          foreach ( var categoriaId in peliculaDTO.Categorias)
          {
            Categoria? categoria = await _context.Categoria.FindAsync(categoriaId);
            if (categoria != null)
               pelicula.Categorias.Add(categoria);
            
          }
        }
        _context.Pelicula.Add(pelicula);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPelicula), new{ id = pelicula.PeliculaId}, pelicula);
}

    //put: api/peliculas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPelicula(int id, PeliculaDTO peliculaDTO)
    {
      if (id != peliculaDTO.PeliculaId)
      {
        return BadRequest();
      }
       var pelicula= await _context.Pelicula.Include(i => i.Categorias).FirstOrDefaultAsync(s => s.PeliculaId ==id);
       if(pelicula ==null )
       {
        return NotFound();
       }
       pelicula.Titulo = peliculaDTO.Titulo;
       pelicula.Sinopsis = peliculaDTO.Sinopsis;
       pelicula.Anio = peliculaDTO.Anio;
       pelicula.Poster = peliculaDTO.Poster;
       pelicula.Categorias = [];
       
        if (peliculaDTO.Categorias !=null)
        {
        foreach (var categoriaId in peliculaDTO.Categorias)
        {
           Categoria? categoria = await _context.Categoria.FindAsync(categoriaId);
           if (categoria !=null)
           pelicula.Categorias.Add(categoria);
        }
        }
       //aqui colocamos el try por si alguien elimina el registro mientras lo usamos
       try{
        await _context.SaveChangesAsync();
       } 
       catch (DbException ex)
       {
        Console.WriteLine(ex.Message);
        return BadRequest();
       }
       return NoContent();
    }
    //delete: api/peliculas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePelicula(int id)
    {
        var pelicula = await _context.Pelicula.FindAsync(id);
        if (pelicula == null)
        {  
             return NotFound();
        }
        _context.Pelicula.Remove(pelicula);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}