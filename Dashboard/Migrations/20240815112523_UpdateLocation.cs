using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dashboard.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Locations");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Locations",
                newName: "Country");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Settings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CityName",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Locations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ZipCode",
                table: "Locations",
                type: "int",
                nullable: false,
                defaultValue: 0);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Users_UserId1",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Settings_Users_UserId1",
                table: "Settings");

            migrationBuilder.DropIndex(
                name: "IX_Settings_UserId1",
                table: "Settings");

            migrationBuilder.DropIndex(
                name: "IX_Locations_UserId1",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "CityName",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Locations");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Locations",
                newName: "City");

            migrationBuilder.AddColumn<float>(
                name: "Latitude",
                table: "Locations",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Longitude",
                table: "Locations",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
