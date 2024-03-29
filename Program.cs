using Microsoft.EntityFrameworkCore;
using backendnet.Data;

var builder = WebApplication.CreateBuilder(args);
//agrega el soporte para mysql 
var connectionString = builder.Configuration.GetConnectionString("DataContext");
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
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
//establece el uso de rutas sin especificar una por default 
app.MapControllers();
//app.MapGet("/", () => "Hello World!");

app.Run();
