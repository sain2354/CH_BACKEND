using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CH_BACKEND.Migrations
{
    public partial class AgregarMetodoEntregaYRecogeEnVenta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "metodo_entrega",
                table: "Venta",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "delivery");

            migrationBuilder.AddColumn<string>(
                name: "sucursal_recoge",
                table: "Venta",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "metodo_entrega",
                table: "Venta");

            migrationBuilder.DropColumn(
                name: "sucursal_recoge",
                table: "Venta");
        }
    }
}
