using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CH_BACKEND.Migrations
{
    public partial class AddTallaProductoEnriquecido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1) Eliminamos FK y PK antiguas de Talla_Producto
            migrationBuilder.DropForeignKey(
                name: "FK_TallaProducto_Talla",
                table: "Talla_Producto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Talla_Producto",
                table: "Talla_Producto");

            // 2) Renombramos id_talla → eur
            migrationBuilder.RenameColumn(
                name: "id_talla",
                table: "Talla_Producto",
                newName: "eur");

            // 3) Aseguramos id_producto NOT NULL (anula posibles default constraints)
            migrationBuilder.AlterColumn<int>(
                name: "id_producto",
                table: "Talla_Producto",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            // 4) Añadimos las nuevas columnas de Talla_Producto
            migrationBuilder.AddColumn<int>(
                name: "usa",
                table: "Talla_Producto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "cm",
                table: "Talla_Producto",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);

            // Nota: omitimos 'stock' pues ya existe

            // 5) Creamos la nueva PK compuesta
            migrationBuilder.AddPrimaryKey(
                name: "PK_TallaProducto",
                table: "Talla_Producto",
                columns: new[] { "id_producto", "usa" });

            // 6) Creamos índice en Talla.descripcion (opcional)
            migrationBuilder.CreateIndex(
                name: "IX_Talla_Descripcion",
                table: "Talla",
                column: "descripcion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // 1) Eliminamos la nueva PK y el índice en Talla
            migrationBuilder.DropPrimaryKey(
                name: "PK_TallaProducto",
                table: "Talla_Producto");

            migrationBuilder.DropIndex(
                name: "IX_Talla_Descripcion",
                table: "Talla");

            // 2) Quitamos solo las columnas que añadimos en Up
            migrationBuilder.DropColumn(
                name: "usa",
                table: "Talla_Producto");

            migrationBuilder.DropColumn(
                name: "cm",
                table: "Talla_Producto");

            // 3) Renombramos eur → id_talla
            migrationBuilder.RenameColumn(
                name: "eur",
                table: "Talla_Producto",
                newName: "id_talla");

            // 4) Revertimos alteración de id_producto
            migrationBuilder.AlterColumn<int>(
                name: "id_producto",
                table: "Talla_Producto",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            // 5) Restauramos la PK original
            migrationBuilder.AddPrimaryKey(
                name: "PK_Talla_Producto",
                table: "Talla_Producto",
                columns: new[] { "id_producto", "id_talla" });

            // No tocamos ninguna columna de Producto en Down porque no las modificamos aquí
        }
    }
}
