using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentIT.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class itemModelUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Items",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CreatorId",
                table: "Items",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AspNetUsers_CreatorId",
                table: "Items",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_AspNetUsers_CreatorId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_CreatorId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Items");

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Description", "ImageUrl", "IsGift", "Name", "RentTime" },
                values: new object[,]
                {
                    { 1, "Fudbalska lopta koju ne koristim vise", "", false, "Lopta", 10 },
                    { 2, "", "", false, "Set knjiga Hari Poter", 60 },
                    { 3, "Trouglasta Lara Kroft, pozajmica ko zeli da igra", "", false, "Tomb Raider PS1", 20 }
                });
        }
    }
}
