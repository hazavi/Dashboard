using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dashboard.Migrations
{
    public partial class addLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key constraint if it exists
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT * 
                    FROM sys.foreign_keys 
                    WHERE name = 'FK_Locations_Users_UserId'
                )
                BEGIN
                    ALTER TABLE [Locations] DROP CONSTRAINT [FK_Locations_Users_UserId]
                END");

            // Drop index if it exists
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT * 
                    FROM sys.indexes 
                    WHERE name = 'IX_Locations_UserId'
                      AND object_id = OBJECT_ID('Locations')
                )
                BEGIN
                    DROP INDEX [IX_Locations_UserId] ON [Locations]
                END");

            // Remove UserId1 column if it exists
            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Locations");

            // Update column types
            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Locations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "CityName",
                table: "Locations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false);

            // Create new index
            migrationBuilder.CreateIndex(
                name: "IX_Locations_UserId",
                table: "Locations",
                column: "UserId",
                unique: true);

            // Add foreign key constraint
            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Users_UserId",
                table: "Locations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop new foreign key constraint if it exists
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT * 
                    FROM sys.foreign_keys 
                    WHERE name = 'FK_Locations_Users_UserId'
                )
                BEGIN
                    ALTER TABLE [Locations] DROP CONSTRAINT [FK_Locations_Users_UserId]
                END");

            // Drop index if it exists
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT * 
                    FROM sys.indexes 
                    WHERE name = 'IX_Locations_UserId'
                      AND object_id = OBJECT_ID('Locations')
                )
                BEGIN
                    DROP INDEX [IX_Locations_UserId] ON [Locations]
                END");

            // Revert column types
            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "CityName",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false);

            // Add column UserId1
            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Locations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // Create index on UserId1
            migrationBuilder.CreateIndex(
                name: "IX_Locations_UserId1",
                table: "Locations",
                column: "UserId1");

            // Add foreign key constraint on UserId1
            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Users_UserId1",
                table: "Locations",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
