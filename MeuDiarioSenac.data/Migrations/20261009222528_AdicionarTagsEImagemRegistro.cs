using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeuDiarioSenac.data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarTagsEImagemRegistro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imagem_url",
                table: "registros",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "tags",
                table: "registros",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imagem_url",
                table: "registros");

            migrationBuilder.DropColumn(
                name: "tags",
                table: "registros");
        }
    }
}
