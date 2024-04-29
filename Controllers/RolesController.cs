using backendnet.Data;
using backendnet.Models; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendnet.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class RolesController : Controller
{
 private readonly IdentityContext _context;

 public  RolesController(IdentityContext context)
 {
    _context = context;
 }
 //Get: api/roles
 [HttpGet]
 public async Task<ActionResult<IEnumerable<UserRolDTO>>> GetRoles()
 {
    var roles = new List<UserRolDTO>();

    foreach (var rol in await _context.Roles.AsNoTracking().ToListAsync())
    {
        roles.Add(new UserRolDTO
        {
            Id = rol.Id,
            Nombre = rol.Nombre,
        });
    }
    return roles;
 }
}
