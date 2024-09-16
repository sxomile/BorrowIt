using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentIT.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUserModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "CityFromIDId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityOfResidenceId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CityFromIDId",
                table: "AspNetUsers",
                column: "CityFromIDId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CityOfResidenceId",
                table: "AspNetUsers",
                column: "CityOfResidenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cities_CityFromIDId",
                table: "AspNetUsers",
                column: "CityFromIDId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cities_CityOfResidenceId",
                table: "AspNetUsers",
                column: "CityOfResidenceId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cities_CityFromIDId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cities_CityOfResidenceId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CityFromIDId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CityOfResidenceId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CityFromIDId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CityOfResidenceId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
