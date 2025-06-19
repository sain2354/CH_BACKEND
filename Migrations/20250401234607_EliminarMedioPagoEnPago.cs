using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CH_BACKEND.Migrations
{
    /// <inheritdoc />
    public partial class EliminarMedioPagoEnPago : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Elimina la clave foránea
            migrationBuilder.DropForeignKey(
                name: "FK__Pago__id_medio_p__656C112C",  // Usa el nombre correcto de la restricción
                table: "Pago");

            // Verifica si el índice existe antes de eliminarlo
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Pago_id_medio_pago' AND object_id = OBJECT_ID('Pago'))
                BEGIN
                    DROP INDEX IX_Pago_id_medio_pago ON Pago;
                END");

            // Elimina la columna 'id_medio_pago'
            migrationBuilder.DropColumn(
                name: "id_medio_pago",
                table: "Pago");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Vuelve a agregar la columna 'id_medio_pago'
            migrationBuilder.AddColumn<int>(
                name: "id_medio_pago",
                table: "Pago",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // Vuelve a crear el índice 'IX_Pago_id_medio_pago'
            migrationBuilder.CreateIndex(
                name: "IX_Pago_id_medio_pago",
                table: "Pago",
                column: "id_medio_pago");

            // Vuelve a agregar la clave foránea
            migrationBuilder.AddForeignKey(
                name: "FK__Pago__id_medio_p__656C112C",  // Usa el nombre correcto de la restricción
                table: "Pago",
                column: "id_medio_pago",
                principalTable: "MedioPago",
                principalColumn: "id_medio_pago");
        }
    }
}
