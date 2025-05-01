using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesWebMvc.Migrations
{
    /// <inheritdoc />
    public partial class AjusteTipoTreino : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoTreinoDescricao",
                table: "Treino");

            migrationBuilder.RenameColumn(
                name: "TipoTreinoId",
                table: "Treino",
                newName: "TipoTreino");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TipoTreino",
                table: "Treino",
                newName: "TipoTreinoId");

            migrationBuilder.AddColumn<string>(
                name: "TipoTreinoDescricao",
                table: "Treino",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
