using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentIT.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DBFillingItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Description", "IsGift", "Name", "RentTime" },
                values: new object[,]
                {
                    { 1, "Fudbalska lopta koju ne koristim vise", false, "Lopta", 10 },
                    { 2, "", false, "Set knjiga Hari Poter", 60 },
                    { 3, "Trouglasta Lara Kroft, pozajmica ko zeli da igra", false, "Tomb Raider PS1", 20 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
