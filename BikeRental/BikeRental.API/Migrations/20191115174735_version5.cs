using Microsoft.EntityFrameworkCore.Migrations;

namespace BikeRental.API.Migrations
{
    public partial class version5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RentalId",
                table: "Customers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentalId",
                table: "Customers");
        }
    }
}
