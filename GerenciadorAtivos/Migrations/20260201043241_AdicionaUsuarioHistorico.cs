using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorAtivos.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaUsuarioHistorico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Usuario",
                table: "Historicos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Usuario",
                table: "Historicos");
        }
    }
}
