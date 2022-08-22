using Microsoft.EntityFrameworkCore.Migrations;

namespace RealStateApp.Infrastructure.Persistence.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mejoras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mejoras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoPropiedades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPropiedades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoVentas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoVentas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Propiedades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdTipoPropiedad = table.Column<int>(type: "int", nullable: false),
                    IdTipoVenta = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<float>(type: "real", nullable: false),
                    Terreno = table.Column<float>(type: "real", nullable: false),
                    CantHabitaciones = table.Column<int>(type: "int", nullable: false),
                    CantLavabos = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdAgente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Imagen1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Imagen2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Imagen3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Imagen4 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propiedades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Propiedades_TipoPropiedades_IdTipoPropiedad",
                        column: x => x.IdTipoPropiedad,
                        principalTable: "TipoPropiedades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Propiedades_TipoVentas_IdTipoVenta",
                        column: x => x.IdTipoVenta,
                        principalTable: "TipoVentas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MejorasPropiedades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPropiedad = table.Column<int>(type: "int", nullable: false),
                    IdMejora = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MejorasPropiedades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MejorasPropiedades_Mejoras_IdMejora",
                        column: x => x.IdMejora,
                        principalTable: "Mejoras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MejorasPropiedades_Propiedades_IdPropiedad",
                        column: x => x.IdPropiedad,
                        principalTable: "Propiedades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MejorasPropiedades_IdMejora",
                table: "MejorasPropiedades",
                column: "IdMejora");

            migrationBuilder.CreateIndex(
                name: "IX_MejorasPropiedades_IdPropiedad",
                table: "MejorasPropiedades",
                column: "IdPropiedad");

            migrationBuilder.CreateIndex(
                name: "IX_Propiedades_IdTipoPropiedad",
                table: "Propiedades",
                column: "IdTipoPropiedad");

            migrationBuilder.CreateIndex(
                name: "IX_Propiedades_IdTipoVenta",
                table: "Propiedades",
                column: "IdTipoVenta");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MejorasPropiedades");

            migrationBuilder.DropTable(
                name: "Mejoras");

            migrationBuilder.DropTable(
                name: "Propiedades");

            migrationBuilder.DropTable(
                name: "TipoPropiedades");

            migrationBuilder.DropTable(
                name: "TipoVentas");
        }
    }
}
