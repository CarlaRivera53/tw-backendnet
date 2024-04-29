using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading;
using backendnet.Data;
using backendnet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace backendnet.Controllers;

[Route("api/[controller]")]
[ApiController]

public class AuthController : Controller
{
    private readonly IdentityContext _context;

    private readonly IConfiguration _configuration;

    private readonly UserManager<CustomIdentityUser> userManager;

    public AuthController(IdentityContext context, UserManager<CustomIdentityUser> userManager,IConfigurationSection configuration)
    {
        _context = context;
        _userManager = userManager;
        _configuration = configuration;
    }
    //POST   api/auth 
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody]LoginDtO loginDTO){
        //verificamos credenciales con Identity 
        var usuario = await _userManager.FindByEmailAsync(loginDTO.Email);

        if (usuario is null || !await _userManager.CheckPasswordAsync(usuario, loginDTO.Password))
        {
            //regresa 401 acceso no autorizado 
            return Unauthorized(new {mensaje = "usuario o contrasña incorrectos. "});
        }
        //estos valores nos indicaran el usuario aunticado en cada peticion usando el token 
        var Claims = new List<Claim>
        {
            new(ClaimTypes.Name,usuario.Email!),
            new(ClaimTypes.GivenName, usuario.Nombre)
        };
        //obtenemos los roles y los agregamos a los claims 
        var roles = await _userManager.GetRolesAsync(usuario);
        foreach (var role in roles)
        {
            Claims.Add(new Claim(ClaimTypes.Role, role));
        }
        //creamos el tojen de acceso de 20 minutos 
        var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!));
        var credentials =  new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken (
           issuer: _configuration["Jwt:Issuer"],
           audience: _configuration["Jwt:Audience"],
           claims: Claims,
           expires: DateTime.Now.AddMinutes(20),
           signingCredentials: credentials 
        var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
    //le regresa su token de acceso al usuario con validez de 20 minutos 
    return Ok(new
    {
        usuario.Email,
        usuario.Nombre,
        rol = string.Join(",", roles),
        jwt
    });
}