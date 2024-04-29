using backendnet.Data;
using backendnet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backendnet.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UsuariosController : Controller
    {
        private readonly IdentityContext _context;
        private readonly UserManager<CustomIdentityUser> _userManager;

        public UsuariosController(IdentityContext context, UserManager<CustomIdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomIdentityUserDTO>>> GetUsuarios()
        {
            var usuarios = new List<CustomIdentityUserDTO>();

            foreach (var usuario in await _context.Users.AsNoTracking().ToListAsync())
            {
                usuarios.Add(new CustomIdentityUserDTO
                {
                    Id = usuario.Id,
                    Nombre = usuario.Nombre,
                    Email = usuario.Email,
                    Rol = GetUserRol(usuario)
                });
            }

            return Ok(usuarios);
        }

        // GET: api/usuarios/email
        [HttpGet("{email}")]
        public async Task<ActionResult<CustomIdentityUserDTO>> GetUsuarioByEmail(string email)
        {
            var usuario = await _userManager.FindByEmailAsync(email);
            if (usuario == null) return NotFound();

            return new CustomIdentityUserDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Email = usuario.Email!,
                Rol = GetUserRol(usuario)
            };
        }
     // POST: api/usuario
     [HttpPost]

    // POST: api/usuario
[HttpPost]
public async Task<ActionResult<CustomIdentityUserDTO>> PostUsuario(CustomIdentityUserDTO usuarioDTO)
{
    var usuarioToCreate = new CustomIdentityUser
    {
        UserName = usuarioDTO.Email,
        Email = usuarioDTO.Email,
        NormalizedEmail = usuarioDTO.Email.ToUpper(),
        Nombre = usuarioDTO.Nombre,
        NormalizedUserName = usuarioDTO.Email.ToUpper()
    };

    // Agrega al usuario
    IdentityResult result = await _userManager.CreateAsync(usuarioToCreate, usuarioDTO.Password);
    if (!result.Succeeded) return BadRequest(new { mensaje = "El usuario no se ha podido crear." });

    // Verifica si el rol existe antes de agregarlo al usuario
    if (await _userManager.FindByEmailAsync(usuarioDTO.Email) == null)
    {
        return NotFound(new { mensaje = "El usuario no existe." });
    }

    // Agrega al rol deseado
    result = await _userManager.AddToRoleAsync(usuarioToCreate, usuarioDTO.Rol);
    if (!result.Succeeded) return BadRequest(new { mensaje = "El usuario se ha creado pero no se ha podido asignar el rol." });

    // Regresa el usuario creado
    var usuarioViewModel = new CustomIdentityUserDTO
    {
        Id = usuarioToCreate.Id,
        Nombre = usuarioDTO.Nombre,
        Email = usuarioDTO.Email,
        Rol = usuarioDTO.Rol
    };

    return CreatedAtAction(nameof(GetUsuarioByEmail), new { email = usuarioDTO.Email }, usuarioViewModel);
}
//PUT: api/usuarios/email
[HttpPut("{email}")]
public async Task<IActionResult> PutUsuario(string email, CustomIdentityUserDTO usuarioDTO)
{
    if (email != usuarioDTO.Email) return BadRequest();

    var usuario = await _userManager.FindByEmailAsync(email);
    if (usuario == null) return NotFound();

    // Verifica que exista el rol recibido
    if (await _context.Roles.Where(r => r.Name == usuarioDTO.Rol).FirstOrDefaultAsync() == null) return NotFound();

    // Actualiza los datos
    usuario.Nombre = usuarioDTO.Nombre;
    usuario.NormalizedUserName = usuarioDTO.Email.ToUpper();
    IdentityResult result = await _userManager.UpdateAsync(usuario);
    if (!result.Succeeded) return BadRequest("Error al actualizar el usuario.");

    // Actualiza el rol seleccionado
    if (!await _userManager.IsInRoleAsync(usuario, usuarioDTO.Rol))
    {
        await _userManager.AddToRoleAsync(usuario, usuarioDTO.Rol);
    }

    return NoContent();
}
//DELETE: api/usuarios/email
[HttpDelete("{email}")]

public async Task<IActionResult> DeleteUsuario(string email)
{
    var usuario = await _userManager.FindByEmailAsync(email);
    if(usuario == null) return NotFound();

    if (usuario == null) return StatusCode(StatusCodes.Status403Forbidden);

    IdentityResult result = await _userManager.DeleteAsync(usuario);
    if(!result.Succeeded) return BadRequest();

    return NoContent();
}
private string GetUserRol(CustomIdentityUser usuario)
{
    var roles = _userManager.GetRolesAsync(usuario).Result;
    return roles.First();
}
    }
}

