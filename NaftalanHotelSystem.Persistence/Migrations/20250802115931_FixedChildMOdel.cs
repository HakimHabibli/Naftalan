using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NaftalanHotelSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixedChildMOdel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomChildren_Children_ChildrenId",
                table: "RoomChildren");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomChildren_Rooms_RoomsId",
                table: "RoomChildren");

            migrationBuilder.RenameColumn(
                name: "RoomsId",
                table: "RoomChildren",
                newName: "ChildId");

            migrationBuilder.RenameColumn(
                name: "ChildrenId",
                table: "RoomChildren",
                newName: "RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomChildren_RoomsId",
                table: "RoomChildren",
                newName: "IX_RoomChildren_ChildId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomChildren_Children_ChildId",
                table: "RoomChildren",
                column: "ChildId",
                principalTable: "Children",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomChildren_Rooms_RoomId",
                table: "RoomChildren",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomChildren_Children_ChildId",
                table: "RoomChildren");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomChildren_Rooms_RoomId",
                table: "RoomChildren");

            migrationBuilder.RenameColumn(
                name: "ChildId",
                table: "RoomChildren",
                newName: "RoomsId");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "RoomChildren",
                newName: "ChildrenId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomChildren_ChildId",
                table: "RoomChildren",
                newName: "IX_RoomChildren_RoomsId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomChildren_Children_ChildrenId",
                table: "RoomChildren",
                column: "ChildrenId",
                principalTable: "Children",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomChildren_Rooms_RoomsId",
                table: "RoomChildren",
                column: "RoomsId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
