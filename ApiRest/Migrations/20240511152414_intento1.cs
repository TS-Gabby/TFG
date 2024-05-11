using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiRest.Migrations
{
    /// <inheritdoc />
    public partial class intento1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Etiqueta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etiqueta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Juego",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Compania = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Accesible_PC = table.Column<bool>(type: "bit", nullable: false),
                    Accesible_M = table.Column<bool>(type: "bit", nullable: false),
                    Descuento = table.Column<float>(type: "real", nullable: false),
                    Precio = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Juego", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JuegoXEtiqueta",
                columns: table => new
                {
                    Id_Juego = table.Column<int>(type: "int", nullable: false),
                    Id_Etiqueta = table.Column<int>(type: "int", nullable: false),
                    JuegoId = table.Column<int>(type: "int", nullable: false),
                    EtiquetaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JuegoXEtiqueta", x => new { x.Id_Juego, x.Id_Etiqueta });
                    table.ForeignKey(
                        name: "FK_JuegoXEtiqueta_Etiqueta_EtiquetaId",
                        column: x => x.EtiquetaId,
                        principalTable: "Etiqueta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JuegoXEtiqueta_Juego_JuegoId",
                        column: x => x.JuegoId,
                        principalTable: "Juego",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioXJuego",
                columns: table => new
                {
                    Id_Usuario = table.Column<int>(type: "int", nullable: false),
                    Id_Juego = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    JuegoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioXJuego", x => new { x.Id_Usuario, x.Id_Juego });
                    table.ForeignKey(
                        name: "FK_UsuarioXJuego_Juego_JuegoId",
                        column: x => x.JuegoId,
                        principalTable: "Juego",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioXJuego_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JuegoXEtiqueta_EtiquetaId",
                table: "JuegoXEtiqueta",
                column: "EtiquetaId");

            migrationBuilder.CreateIndex(
                name: "IX_JuegoXEtiqueta_JuegoId",
                table: "JuegoXEtiqueta",
                column: "JuegoId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioXJuego_JuegoId",
                table: "UsuarioXJuego",
                column: "JuegoId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioXJuego_UsuarioId",
                table: "UsuarioXJuego",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JuegoXEtiqueta");

            migrationBuilder.DropTable(
                name: "UsuarioXJuego");

            migrationBuilder.DropTable(
                name: "Etiqueta");

            migrationBuilder.DropTable(
                name: "Juego");
        }
    }
}
