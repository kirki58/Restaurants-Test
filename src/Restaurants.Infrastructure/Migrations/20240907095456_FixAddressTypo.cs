using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurants.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixAddressTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Adress_Street",
                table: "Restaurants",
                newName: "Address_Street");

            migrationBuilder.RenameColumn(
                name: "Adress_PostalCode",
                table: "Restaurants",
                newName: "Address_PostalCode");

            migrationBuilder.RenameColumn(
                name: "Adress_City",
                table: "Restaurants",
                newName: "Address_City");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address_Street",
                table: "Restaurants",
                newName: "Adress_Street");

            migrationBuilder.RenameColumn(
                name: "Address_PostalCode",
                table: "Restaurants",
                newName: "Adress_PostalCode");

            migrationBuilder.RenameColumn(
                name: "Address_City",
                table: "Restaurants",
                newName: "Adress_City");
        }
    }
}
