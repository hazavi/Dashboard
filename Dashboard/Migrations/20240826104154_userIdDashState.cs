using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dashboard.Migrations
{
    /// <inheritdoc />
    public partial class userIdDashState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Check if the primary key constraint exists before dropping it
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'PK_DashboardStates')
                BEGIN
                    ALTER TABLE [DashboardStates] DROP CONSTRAINT [PK_DashboardStates];
                END
            ");

            // Check if the foreign key constraint exists before dropping it
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_DashboardStates_Users_UserId')
                BEGIN
                    ALTER TABLE [DashboardStates] DROP CONSTRAINT [FK_DashboardStates_Users_UserId];
                END
            ");

            // Alter the column type from string to int
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "DashboardStates",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            // Add the foreign key constraint with the updated column type
            migrationBuilder.AddForeignKey(
                name: "FK_DashboardStates_Users_UserId",
                table: "DashboardStates",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            // Recreate the primary key constraint with the updated column type
            migrationBuilder.AddPrimaryKey(
                name: "PK_DashboardStates",
                table: "DashboardStates",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the new primary key constraint
            migrationBuilder.DropPrimaryKey(
                name: "PK_DashboardStates",
                table: "DashboardStates");

            // Drop the new foreign key constraint
            migrationBuilder.DropForeignKey(
                name: "FK_DashboardStates_Users_UserId",
                table: "DashboardStates");

            // Revert column type back to string
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "DashboardStates",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            // Recreate the old primary key constraint with the original column type
            migrationBuilder.AddPrimaryKey(
                name: "PK_DashboardStates",
                table: "DashboardStates",
                column: "UserId");

            // Re-add the old foreign key constraint if needed
            migrationBuilder.AddForeignKey(
                name: "FK_DashboardStates_Users_UserId",
                table: "DashboardStates",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

    }
}
