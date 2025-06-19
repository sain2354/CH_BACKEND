using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CH_BACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AgregarPreferenceIdMPEnPago : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "preference_id_mp",
                table: "Pago",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "preference_id_mp",
                table: "Pago");
        }
    }
}
