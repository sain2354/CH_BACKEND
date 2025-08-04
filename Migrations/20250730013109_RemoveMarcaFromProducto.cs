using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CH_BACKEND.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMarcaFromProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Eliminar la columna Marca de la tabla Producto
            migrationBuilder.DropColumn(
                name: "Marca",
                table: "Producto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Volver a agregar la columna Marca en caso de reversión
            migrationBuilder.AddColumn<string>(
                name: "Marca",
                table: "Producto",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
