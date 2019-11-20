using Microsoft.EntityFrameworkCore.Migrations;

namespace BikeRental.API.Migrations
{
    public partial class version6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CurrentlyRented",
                table: "Bikes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentlyRented",
                table: "Bikes");
        }
    }
}
