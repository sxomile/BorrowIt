using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentIT.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class IsReturnedadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReturned",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReturned",
                table: "Orders");
        }
    }
}
