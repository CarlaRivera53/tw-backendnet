using backendnet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backendnet.Data.Seed;

public class SeedCategoria : IEntityTypeConfiguration<Categoria>
{

public void Configure(EntityTypeBuilder<Categoria> builder)
{
    builder.HasData(
   new Categoria{ CategoriaId= 1, Nombre ="accion", Protegida = true},
   new Categoria{ CategoriaId= 2, Nombre ="aventura", Protegida = true},
   new Categoria{ CategoriaId= 3, Nombre ="animacion", Protegida = true},
   new Categoria{ CategoriaId= 4, Nombre ="ciencia ficcion", Protegida = true},
   new Categoria{ CategoriaId= 5, Nombre ="comedia", Protegida = true},
   new Categoria{ CategoriaId= 6, Nombre ="criemen", Protegida = true},
   new Categoria{ CategoriaId= 7, Nombre ="documental", Protegida = true},
   new Categoria{ CategoriaId= 8, Nombre ="drama", Protegida = true},
   new Categoria{ CategoriaId= 9, Nombre ="familiar", Protegida = true},
   new Categoria{ CategoriaId= 10, Nombre ="fantasia", Protegida = true},
   new Categoria{ CategoriaId= 11, Nombre ="historia", Protegida = true},
   new Categoria{ CategoriaId= 12, Nombre ="horror", Protegida = true},
   new Categoria{ CategoriaId= 13, Nombre ="musica", Protegida = true},
   new Categoria{ CategoriaId= 14, Nombre ="misterio", Protegida = true},
   new Categoria{ CategoriaId= 15, Nombre ="romance", Protegida = true}
    );
}
}