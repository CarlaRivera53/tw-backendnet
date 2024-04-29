using backendnet.Data.Seed;
using backendnet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;
namespace backendnet.Data;

public class IdentityContext : DbContext
{
  public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
  {

  }  

  public DbSet<Pelicula> Pelicula {get; set;}
  public DbSet<Categoria> Categoria {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SeedCategoria());
        modelBuilder.ApplyConfiguration( new SeedPelicula());
        modelBuilder.SeedUserIdentityData();

        base.OnModelCreating(modelBuilder);
    }

}