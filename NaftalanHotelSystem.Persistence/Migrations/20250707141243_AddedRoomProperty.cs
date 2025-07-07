using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NaftalanHotelSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedRoomProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MiniDescription",
                table: "RoomTranslations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiniTitle",
                table: "RoomTranslations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "RoomTranslations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MiniDescription",
                table: "RoomTranslations");

            migrationBuilder.DropColumn(
                name: "MiniTitle",
                table: "RoomTranslations");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "RoomTranslations");
        }
    }
}
