using Microsoft.EntityFrameworkCore;
using backendnet.Data;
using backendnet.Models;
using backendnet.AspNetCore.Identity; 
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Agrega el soporte para MySQL
var connectionString = builder.Configuration.GetConnectionString("IdentityContext");
builder.Services.AddDbContext<IdentityContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    options.UserRequireUniqueEmail = true;
    // Cambia aquí como quieres que se manejen tus contraseñas
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredUniqueChars = 1;
})

.AddEntityFrameworkStores<IdentityContext>();

//soporte para JWT 
builder.Services
.AddHttpContextAccessor() // para poder acceder al HttpContext()
.AddAuthorization() //para autorizar en cadametido el acceso 
.AddAuthentication(options =>
{
options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => //para autenticar con JWT
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt: Issuer"], //leido desde appsettings
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!))

    };
});
//agrega la funcionalidad de controladores 
builder.Services.AddControllers();
//agrega la documentacion de la api 
builder.Services.AddSwaggerGen();
//contruye la aplicacion web 
var app = builder.Build();

//si queremos mostrar la documentacion de la api en la raiz 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "backend v1");
        options.RoutePrefix = string.Empty;
    });
}

//redirige a https 
app.UseHttpsRedirection();
//utiliza rutas para los endpoints de los contraladores
app.UseRouting();
//utiliza autenticacion 
app.UseAuthorization();
//utiliza autorizacion 
app.UseAuthorization();

//usa cors en la plicydefinida anteriormente 
app.UseCors();

//establece el uso de rutas sin especificar una por default 
app.MapControllers();
//app.MapGet("/", () => "Hello World!");

app.Run();
