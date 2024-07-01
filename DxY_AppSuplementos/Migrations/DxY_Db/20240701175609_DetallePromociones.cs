using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DxY_AppSuplementos.Migrations.DxY_Db
{
    /// <inheritdoc />
    public partial class DetallePromociones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DetallePromociones",
                columns: table => new
                {
                    DetallePromocionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Disponibilidad = table.Column<int>(type: "int", nullable: false),
                    Imagen = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    TipoImagen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreImagen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PromocionID = table.Column<int>(type: "int", nullable: false),
                    ProductoID = table.Column<int>(type: "int", nullable: false),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallePromociones", x => x.DetallePromocionID);
                    table.ForeignKey(
                        name: "FK_DetallePromociones_Productos_ProductoID",
                        column: x => x.ProductoID,
                        principalTable: "Productos",
                        principalColumn: "ProductoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallePromociones_Promociones_PromocionID",
                        column: x => x.PromocionID,
                        principalTable: "Promociones",
                        principalColumn: "PromocionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallePromociones_ProductoID",
                table: "DetallePromociones",
                column: "ProductoID");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePromociones_PromocionID",
                table: "DetallePromociones",
                column: "PromocionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallePromociones");
        }
    }
}
