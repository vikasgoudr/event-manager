using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManager.DAL.Migrations
{
    /// <inheritdoc />
    public partial class NumberOfRegistrations_IsPublished : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                schema: "Event",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfRegistrations",
                schema: "Event",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                schema: "Event",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "NumberOfRegistrations",
                schema: "Event",
                table: "Answers");
        }
    }
}
