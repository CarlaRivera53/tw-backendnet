using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace tw_backendnet.Migrations
{
    /// <inheritdoc />
    public partial class CreacionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Protegida = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.CategoriaId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pelicula",
                columns: table => new
                {
                    PeliculaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sinopsis = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Anio = table.Column<int>(type: "int", nullable: false),
                    Poster = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pelicula", x => x.PeliculaId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CategoriaPelicula",
                columns: table => new
                {
                    CategoriasCategoriaId = table.Column<int>(type: "int", nullable: false),
                    PeliculasPeliculaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaPelicula", x => new { x.CategoriasCategoriaId, x.PeliculasPeliculaId });
                    table.ForeignKey(
                        name: "FK_CategoriaPelicula_Categoria_CategoriasCategoriaId",
                        column: x => x.CategoriasCategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoriaPelicula_Pelicula_PeliculasPeliculaId",
                        column: x => x.PeliculasPeliculaId,
                        principalTable: "Pelicula",
                        principalColumn: "PeliculaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "CategoriaId", "Nombre", "Protegida" },
                values: new object[,]
                {
                    { 1, "accion", true },
                    { 2, "aventura", true },
                    { 3, "animacion", true },
                    { 4, "ciencia ficcion", true },
                    { 5, "comedia", true },
                    { 6, "criemen", true },
                    { 7, "documental", true },
                    { 8, "drama", true },
                    { 9, "familiar", true },
                    { 10, "fantasia", true },
                    { 11, "historia", true },
                    { 12, "horror", true },
                    { 13, "musica", true },
                    { 14, "misterio", true },
                    { 15, "romance", true }
                });

            migrationBuilder.InsertData(
                table: "Pelicula",
                columns: new[] { "PeliculaId", "Anio", "Poster", "Sinopsis", "Titulo" },
                values: new object[,]
                {
                    { 1, 0, "N/A", "el banquero  Andy Dufresne es arrestado por matar a su espsosa", "Sueño de fuga" },
                    { 2, 0, "N/A", "el patriarca de una organizacion criminal ", "El padrino" },
                    { 3, 0, "N/A", "cuando el Joker emerge causando caos en cidad Gotica", "El caballero oscuro" },
                    { 4, 0, "N/A", "Gandalf y Aragon lideran el mundo de los hombres", "El retorno del rey " },
                    { 5, 0, "N/A", "la vida de dos mafiosos , un  boxeador", "tiempos violentos" },
                    { 6, 0, "N/A", "Las presidencias de Kenedy y johnson, los eventos de vietnam", "Forrest Gump" },
                    { 7, 0, "N/A", "un hombre deprimido conoce a un fabricante de jabon", "El club de la pelea" },
                    { 8, 0, "N/A", "a un ladron que roba secretos coorporativos a traves de la tecnologia", "Incepcion" },
                    { 9, 0, "N/A", "los rebeldes han vencido", "Star wars: espisodio V - El imperip contrataca" },
                    { 10, 0, "N/A", "un hacker se da cuenta por medio de otro rebeldes de la naturaleza", "Matrix" },
                    { 11, 0, "N/A", "un grupo de exploradores prueban los saltos a traves del espacio", "Interestelar" },
                    { 12, 0, "N/A", "Paul Atreides se une a chani y los fremmen", "Dune: parte dos" },
                    { 13, 0, "N/A", "un cyborg, identico al que fracaso", "terminator 2 : el juicio Final" },
                    { 14, 0, "N/A", "Marty McFly, un estudiante de 17 años ", "volver al futuro" },
                    { 15, 0, "N/A", "vivir en Barbie Land es ser perfecto en un lugar perfecto", "Barbie" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoriaPelicula_PeliculasPeliculaId",
                table: "CategoriaPelicula",
                column: "PeliculasPeliculaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoriaPelicula");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Pelicula");
        }
    }
}
