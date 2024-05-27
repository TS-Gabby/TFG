using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiRest.Migrations
{
    /// <inheritdoc />
    public partial class vamosaello : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JuegoXEtiqueta_Etiqueta_EtiquetaId",
                table: "JuegoXEtiqueta");

            migrationBuilder.DropForeignKey(
                name: "FK_JuegoXEtiqueta_Juego_JuegoId",
                table: "JuegoXEtiqueta");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioXJuego_Juego_JuegoId",
                table: "UsuarioXJuego");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioXJuego_Usuarios_UsuarioId",
                table: "UsuarioXJuego");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioXJuego_JuegoId",
                table: "UsuarioXJuego");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioXJuego_UsuarioId",
                table: "UsuarioXJuego");

            migrationBuilder.DropIndex(
                name: "IX_JuegoXEtiqueta_EtiquetaId",
                table: "JuegoXEtiqueta");

            migrationBuilder.DropIndex(
                name: "IX_JuegoXEtiqueta_JuegoId",
                table: "JuegoXEtiqueta");

            migrationBuilder.DropColumn(
                name: "JuegoId",
                table: "UsuarioXJuego");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "UsuarioXJuego");

            migrationBuilder.DropColumn(
                name: "IdRol",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "EtiquetaId",
                table: "JuegoXEtiqueta");

            migrationBuilder.DropColumn(
                name: "JuegoId",
                table: "JuegoXEtiqueta");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioXJuego_IdJuego",
                table: "UsuarioXJuego",
                column: "IdJuego");

            migrationBuilder.CreateIndex(
                name: "IX_JuegoXEtiqueta_IdEtiqueta",
                table: "JuegoXEtiqueta",
                column: "IdEtiqueta");

            migrationBuilder.AddForeignKey(
                name: "FK_JuegoXEtiqueta_Etiqueta_IdEtiqueta",
                table: "JuegoXEtiqueta",
                column: "IdEtiqueta",
                principalTable: "Etiqueta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JuegoXEtiqueta_Juego_IdJuego",
                table: "JuegoXEtiqueta",
                column: "IdJuego",
                principalTable: "Juego",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioXJuego_Juego_IdJuego",
                table: "UsuarioXJuego",
                column: "IdJuego",
                principalTable: "Juego",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioXJuego_Usuarios_IdUsuario",
                table: "UsuarioXJuego",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JuegoXEtiqueta_Etiqueta_IdEtiqueta",
                table: "JuegoXEtiqueta");

            migrationBuilder.DropForeignKey(
                name: "FK_JuegoXEtiqueta_Juego_IdJuego",
                table: "JuegoXEtiqueta");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioXJuego_Juego_IdJuego",
                table: "UsuarioXJuego");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioXJuego_Usuarios_IdUsuario",
                table: "UsuarioXJuego");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioXJuego_IdJuego",
                table: "UsuarioXJuego");

            migrationBuilder.DropIndex(
                name: "IX_JuegoXEtiqueta_IdEtiqueta",
                table: "JuegoXEtiqueta");

            migrationBuilder.AddColumn<int>(
                name: "JuegoId",
                table: "UsuarioXJuego",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "UsuarioXJuego",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdRol",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EtiquetaId",
                table: "JuegoXEtiqueta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "JuegoId",
                table: "JuegoXEtiqueta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioXJuego_JuegoId",
                table: "UsuarioXJuego",
                column: "JuegoId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioXJuego_UsuarioId",
                table: "UsuarioXJuego",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_JuegoXEtiqueta_EtiquetaId",
                table: "JuegoXEtiqueta",
                column: "EtiquetaId");

            migrationBuilder.CreateIndex(
                name: "IX_JuegoXEtiqueta_JuegoId",
                table: "JuegoXEtiqueta",
                column: "JuegoId");

            migrationBuilder.AddForeignKey(
                name: "FK_JuegoXEtiqueta_Etiqueta_EtiquetaId",
                table: "JuegoXEtiqueta",
                column: "EtiquetaId",
                principalTable: "Etiqueta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JuegoXEtiqueta_Juego_JuegoId",
                table: "JuegoXEtiqueta",
                column: "JuegoId",
                principalTable: "Juego",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioXJuego_Juego_JuegoId",
                table: "UsuarioXJuego",
                column: "JuegoId",
                principalTable: "Juego",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioXJuego_Usuarios_UsuarioId",
                table: "UsuarioXJuego",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
