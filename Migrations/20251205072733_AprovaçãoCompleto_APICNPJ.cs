using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestauranteAPP_TP3.Migrations
{
    /// <inheritdoc />
    public partial class AprovaçãoCompleto_APICNPJ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CNPJ",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CNPJ",
                table: "AspNetUsers");
        }
    }
}
