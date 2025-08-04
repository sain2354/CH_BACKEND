using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CH_BACKEND.Migrations
{
    public partial class AddTallaProductos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Eliminamos la FK antigua con Talla
            migrationBuilder.DropForeignKey(
                name: "FK_CarritoDetalle_Talla",
                table: "Carrito_Detalle");

            // Eliminamos índice en Talla.Descripcion si ya no existe
            migrationBuilder.DropIndex(
                name: "IX_Talla_Descripcion",
                table: "Talla");

            // <<< Quitamos este DropIndex, ya no existe >>>
            // migrationBuilder.DropIndex(
            //     name: "IX_Carrito_Detalle_id_talla",
            //     table: "Carrito_Detalle");

            // Eliminamos la columna descripcion de Talla
            migrationBuilder.DropColumn(
                name: "descripcion",
                table: "Talla");

            // Agregamos las nuevas columnas a Talla
            migrationBuilder.AddColumn<string>(
                name: "categoria",
                table: "Talla",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "cm",
                table: "Talla",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "eur",
                table: "Talla",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "usa",
                table: "Talla",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // Ajustes de orden de columna en Carrito_Detalle (pueden ignorarse)
            migrationBuilder.AlterColumn<int>(
                name: "id_talla",
                table: "Carrito_Detalle",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "id_producto",
                table: "Carrito_Detalle",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "id_carrito",
                table: "Carrito_Detalle",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revertimos columnas de Talla
            migrationBuilder.DropColumn(
                name: "categoria",
                table: "Talla");

            migrationBuilder.DropColumn(
                name: "cm",
                table: "Talla");

            migrationBuilder.DropColumn(
                name: "eur",
                table: "Talla");

            migrationBuilder.DropColumn(
                name: "usa",
                table: "Talla");

            // Restauramos descripcion en Talla
            migrationBuilder.AddColumn<string>(
                name: "descripcion",
                table: "Talla",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            // Revertimos orden de columnas en Carrito_Detalle
            migrationBuilder.AlterColumn<int>(
                name: "id_talla",
                table: "Carrito_Detalle",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "id_producto",
                table: "Carrito_Detalle",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "id_carrito",
                table: "Carrito_Detalle",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 0);

            // Restauramos índice en Talla.Descripcion
            migrationBuilder.CreateIndex(
                name: "IX_Talla_Descripcion",
                table: "Talla",
                column: "descripcion");

            // Ya no recreamos índice ni FK sobre Carrito_Detalle.id_talla
        }
    }
}
