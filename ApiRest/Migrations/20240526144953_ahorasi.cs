using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiRest.Migrations
{
    /// <inheritdoc />
    public partial class ahorasi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id_Juego",
                table: "UsuarioXJuego",
                newName: "IdJuego");

            migrationBuilder.RenameColumn(
                name: "Id_Usuario",
                table: "UsuarioXJuego",
                newName: "IdUsuario");

            migrationBuilder.RenameColumn(
                name: "Id_Etiqueta",
                table: "JuegoXEtiqueta",
                newName: "IdEtiqueta");

            migrationBuilder.RenameColumn(
                name: "Id_Juego",
                table: "JuegoXEtiqueta",
                newName: "IdJuego");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdJuego",
                table: "UsuarioXJuego",
                newName: "Id_Juego");

            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                table: "UsuarioXJuego",
                newName: "Id_Usuario");

            migrationBuilder.RenameColumn(
                name: "IdEtiqueta",
                table: "JuegoXEtiqueta",
                newName: "Id_Etiqueta");

            migrationBuilder.RenameColumn(
                name: "IdJuego",
                table: "JuegoXEtiqueta",
                newName: "Id_Juego");
        }
    }
}
