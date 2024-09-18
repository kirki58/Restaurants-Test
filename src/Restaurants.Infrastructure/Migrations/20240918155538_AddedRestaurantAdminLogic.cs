using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurants.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedRestaurantAdminLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "Restaurants",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql("UPDATE Restaurants SET AdminId = (SELECT TOP 1 Id FROM AspNetUsers)");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_AdminId",
                table: "Restaurants",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_AspNetUsers_AdminId",
                table: "Restaurants",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_AspNetUsers_AdminId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_AdminId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Restaurants");
        }
    }
}
