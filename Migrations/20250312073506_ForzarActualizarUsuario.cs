using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CH_BACKEND.Migrations
{
    /// <inheritdoc />
    public partial class ForzarActualizarUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Usuario",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.AddColumn<string>(
                name: "telefono",
                table: "Usuario",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                defaultValue: "No registrado"
            );

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_registro",
                table: "Usuario",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "email", table: "Usuario");
            migrationBuilder.DropColumn(name: "telefono", table: "Usuario");
            migrationBuilder.DropColumn(name: "fecha_registro", table: "Usuario");
        }
    }
}
