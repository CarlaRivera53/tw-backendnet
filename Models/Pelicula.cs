namespace backendnet.Models;

public class Pelicula
{
  // en este ORM, la llave esla propiedad con la palabra [clase]Id
  public int PeliculaId {get; set;} 
  public string Titulo {get; set;} ="sin titulo";
  public string Sinopsis {get; set;} ="sin sinopsis";
  public int Anio {get; set;}
  public string Poster {get; set;} ="N/A";
  public ICollection<Categoria>? Categorias {get; set;}
}