using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentIT.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RentTimeToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RentTime",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentTime",
                table: "Orders");
        }
    }
}
