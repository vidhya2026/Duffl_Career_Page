using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Duffl_career.Migrations
{
    /// <inheritdoc />
    public partial class AIService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AIResult",
                table: "ContactTable",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AIResult",
                table: "ContactTable");
        }
    }
}
