using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CH_BACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddProductExtraFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Agregar campos extra a la tabla Producto
            migrationBuilder.AddColumn<string>(
                name: "Mpn",
                table: "Producto",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingInfo",
                table: "Producto",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Material",
                table: "Producto",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Producto",
                type: "varchar(50)",
                nullable: true);

            // (Opcional) también podrías agregar ImageUrl si lo necesitas:
            // migrationBuilder.AddColumn<string>(
            //     name: "ImageUrl",
            //     table: "Producto",
            //     type: "varchar(max)",
            //     nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revertir los cambios
            migrationBuilder.DropColumn(
                name: "Mpn",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "ShippingInfo",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "Material",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Producto");

            // Si descomentaste ImageUrl en Up, revierte también:
            // migrationBuilder.DropColumn(
            //     name: "ImageUrl",
            //     table: "Producto");
        }
    }
}
