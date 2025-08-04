using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CH_BACKEND.Migrations
{
    public partial class AddGeneroArticuloMarcaEstiloToProducto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "genero",
                table: "Producto",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "articulo",
                table: "Producto",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "marca",
                table: "Producto",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "estilo",
                table: "Producto",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "estilo",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "marca",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "articulo",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "genero",
                table: "Producto");
        }
    }
}
