using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiEFDBF.Migrations
{
    /// <inheritdoc />
    public partial class cascadefacdetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articulos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    precio = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_articulo", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "forma_pago",
                columns: table => new
                {
                    id_pago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    forma_pago = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_forma_pago", x => x.id_pago);
                });

            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    id_factura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nro_factura = table.Column<int>(type: "int", nullable: true),
                    fecha = table.Column<DateTime>(type: "datetime", nullable: true),
                    id_pago = table.Column<int>(type: "int", nullable: true),
                    cliente = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_factura", x => x.id_factura);
                    table.ForeignKey(
                        name: "fk_factura_forma_pago",
                        column: x => x.id_pago,
                        principalTable: "forma_pago",
                        principalColumn: "id_pago");
                });

            migrationBuilder.CreateTable(
                name: "Detalles_factura",
                columns: table => new
                {
                    id_detalle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_articulo = table.Column<int>(type: "int", nullable: true),
                    id_factura = table.Column<int>(type: "int", nullable: true),
                    precio_unitario = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    cantidad = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_detalle_factura", x => x.id_detalle);
                    table.ForeignKey(
                        name: "fk_detalle_articulo",
                        column: x => x.id_articulo,
                        principalTable: "Articulos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_detalle_factura_factura",
                        column: x => x.id_factura,
                        principalTable: "Facturas",
                        principalColumn: "id_factura",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_factura_id_articulo",
                table: "Detalles_factura",
                column: "id_articulo");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_factura_id_factura",
                table: "Detalles_factura",
                column: "id_factura");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_id_pago",
                table: "Facturas",
                column: "id_pago");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detalles_factura");

            migrationBuilder.DropTable(
                name: "Articulos");

            migrationBuilder.DropTable(
                name: "Facturas");

            migrationBuilder.DropTable(
                name: "forma_pago");
        }
    }
}
