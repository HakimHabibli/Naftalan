using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NaftalanHotelSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPackageEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    DurationDay = table.Column<short>(type: "smallint", nullable: false),
                    RoomType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PackageTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackageTranslations_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackageTreatmentMethod",
                columns: table => new
                {
                    PackagesId = table.Column<int>(type: "int", nullable: false),
                    TreatmentMethodsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageTreatmentMethod", x => new { x.PackagesId, x.TreatmentMethodsId });
                    table.ForeignKey(
                        name: "FK_PackageTreatmentMethod_Packages_PackagesId",
                        column: x => x.PackagesId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackageTreatmentMethod_TreatmentMethods_TreatmentMethodsId",
                        column: x => x.TreatmentMethodsId,
                        principalTable: "TreatmentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PackageTranslations_PackageId",
                table: "PackageTranslations",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageTreatmentMethod_TreatmentMethodsId",
                table: "PackageTreatmentMethod",
                column: "TreatmentMethodsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackageTranslations");

            migrationBuilder.DropTable(
                name: "PackageTreatmentMethod");

            migrationBuilder.DropTable(
                name: "Packages");
        }
    }
}
