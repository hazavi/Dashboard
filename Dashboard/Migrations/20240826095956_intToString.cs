using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dashboard.Migrations
{
    /// <inheritdoc />
    public partial class intToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop primary key constraint
            migrationBuilder.DropPrimaryKey(
                name: "PK_DashboardStates",
                table: "DashboardStates");

            // Drop the column with IDENTITY property
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DashboardStates");

            // Recreate the column without the IDENTITY property
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "DashboardStates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // Recreate the primary key constraint
            migrationBuilder.AddPrimaryKey(
                name: "PK_DashboardStates",
                table: "DashboardStates",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the primary key constraint
            migrationBuilder.DropPrimaryKey(
                name: "PK_DashboardStates",
                table: "DashboardStates");

            // Drop the column without IDENTITY property
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DashboardStates");

            // Recreate the column with IDENTITY property
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "DashboardStates",
                type: "string",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            // Recreate the primary key constraint
            migrationBuilder.AddPrimaryKey(
                name: "PK_DashboardStates",
                table: "DashboardStates",
                column: "UserId");
        }
    }
}
