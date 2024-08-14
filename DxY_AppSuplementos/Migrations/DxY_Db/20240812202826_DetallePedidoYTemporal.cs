using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DxY_AppSuplementos.Migrations.DxY_Db
{
    /// <inheritdoc />
    public partial class DetallePedidoYTemporal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAPagar",
                table: "DetallePedidos");

            migrationBuilder.AddColumn<int>(
                name: "ClienteID",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DetallePedidoTemporales",
                columns: table => new
                {
                    DetallePedidoTemporalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductoID = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallePedidoTemporales", x => x.DetallePedidoTemporalID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ClienteID",
                table: "Pedidos",
                column: "ClienteID");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidos_ProductoID",
                table: "DetallePedidos",
                column: "ProductoID");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePedidos_Productos_ProductoID",
                table: "DetallePedidos",
                column: "ProductoID",
                principalTable: "Productos",
                principalColumn: "ProductoID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Clientes_ClienteID",
                table: "Pedidos",
                column: "ClienteID",
                principalTable: "Clientes",
                principalColumn: "ClienteID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedidos_Productos_ProductoID",
                table: "DetallePedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Clientes_ClienteID",
                table: "Pedidos");

            migrationBuilder.DropTable(
                name: "DetallePedidoTemporales");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_ClienteID",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_DetallePedidos_ProductoID",
                table: "DetallePedidos");

            migrationBuilder.DropColumn(
                name: "ClienteID",
                table: "Pedidos");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAPagar",
                table: "DetallePedidos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
