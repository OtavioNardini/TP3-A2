using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestauranteAPP_TP3.Migrations
{
    /// <inheritdoc />
    public partial class pRODUTOS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atendimentos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Atendimentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoId = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeParceiro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxaParceiro = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TaxaFixa = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atendimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Atendimentos_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_PedidoId",
                table: "Atendimentos",
                column: "PedidoId");
        }
    }
}
