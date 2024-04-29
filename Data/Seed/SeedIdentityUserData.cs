using backendnet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace backendnet.Data.Seed
{
    public static class SeedIdentityUserData
    {
        public static void SeedUserIdentityData(this ModelBuilder modelBuilder)
        {
            // Agregar el rol "Administrador" a la tabla AspNetRoles
            string AdministradorRoleId = Guid.NewGuid().ToString();
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = AdministradorRoleId,
                Name = "Administrador",
                NormalizedName = "Administrador".ToUpper()
            });

            // Agregar un usuario a la tabla AspNetUsers
            var UsuarioId = Guid.NewGuid().ToString();
            modelBuilder.Entity<CustomIdentityUser>().HasData(
                new CustomIdentityUser
                {
                    Id = UsuarioId, // Primary key
                    UserName = "gvera@uv.mx",
                    Email = "gvera@uv.mx",
                    NormalizedEmail = "gvera@uv.mx".ToUpper(),
                    Nombre = "Carla Rivera",
                    NormalizedUserName = "gvera@uv.mx".ToUpper(),
                    PasswordHash = new PasswordHasher<CustomIdentityUser>().HashPassword(null, "patito"),
                    Protegido = true // Este no se puede eliminar 
                }
            );

            // Aplicar la relación entre el usuario y el rol en la tabla AspNetUserRoles
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = UsuarioId,
                    RoleId = AdministradorRoleId
                }
            );
        }
    }
}


