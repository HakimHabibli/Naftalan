using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NaftalanHotelSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedTranslationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TreatmentMethodTranslation_TreatmentMethod_TreatmentMethodId",
                table: "TreatmentMethodTranslation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TreatmentMethodTranslation",
                table: "TreatmentMethodTranslation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TreatmentMethod",
                table: "TreatmentMethod");

            migrationBuilder.RenameTable(
                name: "TreatmentMethodTranslation",
                newName: "TreatmentMethodTranslations");

            migrationBuilder.RenameTable(
                name: "TreatmentMethod",
                newName: "TreatmentMethods");

            migrationBuilder.RenameIndex(
                name: "IX_TreatmentMethodTranslation_TreatmentMethodId",
                table: "TreatmentMethodTranslations",
                newName: "IX_TreatmentMethodTranslations_TreatmentMethodId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TreatmentMethodTranslations",
                table: "TreatmentMethodTranslations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TreatmentMethods",
                table: "TreatmentMethods",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TreatmentMethodTranslations_TreatmentMethods_TreatmentMethodId",
                table: "TreatmentMethodTranslations",
                column: "TreatmentMethodId",
                principalTable: "TreatmentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TreatmentMethodTranslations_TreatmentMethods_TreatmentMethodId",
                table: "TreatmentMethodTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TreatmentMethodTranslations",
                table: "TreatmentMethodTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TreatmentMethods",
                table: "TreatmentMethods");

            migrationBuilder.RenameTable(
                name: "TreatmentMethodTranslations",
                newName: "TreatmentMethodTranslation");

            migrationBuilder.RenameTable(
                name: "TreatmentMethods",
                newName: "TreatmentMethod");

            migrationBuilder.RenameIndex(
                name: "IX_TreatmentMethodTranslations_TreatmentMethodId",
                table: "TreatmentMethodTranslation",
                newName: "IX_TreatmentMethodTranslation_TreatmentMethodId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TreatmentMethodTranslation",
                table: "TreatmentMethodTranslation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TreatmentMethod",
                table: "TreatmentMethod",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TreatmentMethodTranslation_TreatmentMethod_TreatmentMethodId",
                table: "TreatmentMethodTranslation",
                column: "TreatmentMethodId",
                principalTable: "TreatmentMethod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
