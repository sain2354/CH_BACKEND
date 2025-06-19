using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CH_BACKEND.Migrations
{
    /// <inheritdoc />
    public partial class CambioRelacionVenta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Elimina la FK antigua
            migrationBuilder.DropForeignKey(
                name: "FK_Venta_Persona",
                table: "Venta");

            // Renombra la columna de "IdPersona" a "IdUsuario"
            migrationBuilder.RenameColumn(
                name: "IdPersona",
                table: "Venta",
                newName: "IdUsuario");

            // (Opcional) Si hubiera un índice relacionado, se podría renombrar
            // migrationBuilder.RenameIndex(
            //     name: "IX_Ventas_IdPersona",
            //     table: "Venta",
            //     newName: "IX_Venta_IdUsuario");

            // Elimina todos los registros de la tabla Venta
            migrationBuilder.Sql("DELETE FROM Venta");

            // Agrega la nueva FK apuntando a la columna renombrada
            migrationBuilder.AddForeignKey(
                name: "FK_Venta_Usuario",
                table: "Venta",
                column: "IdUsuario",
                principalTable: "Usuario",
                principalColumn: "id_usuario",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Elimina la nueva FK
            migrationBuilder.DropForeignKey(
                name: "FK_Venta_Usuario",
                table: "Venta");

            // (Opcional) Renombra el índice de vuelta si fuera necesario
            // migrationBuilder.RenameIndex(
            //     name: "IX_Venta_IdUsuario",
            //     table: "Venta",
            //     newName: "IX_Ventas_IdPersona");

            // Renombra la columna de vuelta a "IdPersona"
            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                table: "Venta",
                newName: "IdPersona");

            // Vuelve a agregar la FK antigua
            migrationBuilder.AddForeignKey(
                name: "FK_Venta_Persona",
                table: "Venta",
                column: "IdPersona",
                principalTable: "Usuario",
                principalColumn: "id_usuario",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
