using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CH_BACKEND.Migrations
{
    public partial class AgregarCamposEstadoPagoYTransaccionMP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EstadoPago",
                table: "Venta", // Se usa "Venta" en lugar de "Ventas"
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Pendiente"); // Valor por defecto

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "Usuario",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "estado_pago",
                table: "Pago",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "Pendiente");

            migrationBuilder.AddColumn<string>(
                name: "id_transaccion_mp",
                table: "Pago",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoPago",
                table: "Venta");

            migrationBuilder.DropColumn(
                name: "estado_pago",
                table: "Pago");

            migrationBuilder.DropColumn(
                name: "id_transaccion_mp",
                table: "Pago");

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "Usuario",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);
        }
    }
}
