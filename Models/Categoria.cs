using System.Text.Json.Serialization;

namespace backendnet.Models;

public class Categoria
{
    //en este ORM, lallave es la propiedad con la palabra [clase]Id
    public int CategoriaId {get; set;}
    public required string Nombre {get; set;}
    public bool Protegida {get; set;} = false;

    [JsonIgnore]
    public ICollection<Pelicula>? Peliculas {get; set;}
    
}