using backendnet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backendnet.Data.Seed;
public class SeedPelicula : IEntityTypeConfiguration<Pelicula>
{
    public void Configure(EntityTypeBuilder<Pelicula>builder)
    {
        builder.HasData(
            new Pelicula {PeliculaId = 1, Titulo ="Sueño de fuga", Sinopsis ="el banquero  Andy Dufresne es arrestado por matar a su espsosa"},
            new Pelicula {PeliculaId = 2, Titulo ="El padrino", Sinopsis ="el patriarca de una organizacion criminal "},
            new Pelicula {PeliculaId = 3, Titulo ="El caballero oscuro", Sinopsis ="cuando el Joker emerge causando caos en cidad Gotica"},
            new Pelicula {PeliculaId = 4, Titulo ="El retorno del rey ", Sinopsis ="Gandalf y Aragon lideran el mundo de los hombres"},
            new Pelicula {PeliculaId = 5, Titulo ="tiempos violentos", Sinopsis ="la vida de dos mafiosos , un  boxeador"},
            new Pelicula {PeliculaId = 6, Titulo ="Forrest Gump", Sinopsis ="Las presidencias de Kenedy y johnson, los eventos de vietnam"},
            new Pelicula {PeliculaId = 7, Titulo ="El club de la pelea", Sinopsis ="un hombre deprimido conoce a un fabricante de jabon"},
            new Pelicula {PeliculaId = 8, Titulo ="Incepcion", Sinopsis ="a un ladron que roba secretos coorporativos a traves de la tecnologia"},
            new Pelicula {PeliculaId = 9, Titulo ="Star wars: espisodio V - El imperip contrataca", Sinopsis ="los rebeldes han vencido"},
            new Pelicula {PeliculaId = 10, Titulo ="Matrix", Sinopsis ="un hacker se da cuenta por medio de otro rebeldes de la naturaleza"},
            new Pelicula {PeliculaId = 11, Titulo ="Interestelar", Sinopsis ="un grupo de exploradores prueban los saltos a traves del espacio"},
            new Pelicula {PeliculaId = 12, Titulo ="Dune: parte dos", Sinopsis ="Paul Atreides se une a chani y los fremmen"},
            new Pelicula {PeliculaId = 13, Titulo ="terminator 2 : el juicio Final", Sinopsis ="un cyborg, identico al que fracaso"},
            new Pelicula {PeliculaId = 14, Titulo ="volver al futuro", Sinopsis ="Marty McFly, un estudiante de 17 años "},
            new Pelicula {PeliculaId = 15, Titulo ="Barbie", Sinopsis = "vivir en Barbie Land es ser perfecto en un lugar perfecto"}
        );
    }
    
}