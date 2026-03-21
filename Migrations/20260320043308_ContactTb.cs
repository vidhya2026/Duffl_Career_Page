using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Duffl_career.Migrations
{
    /// <inheritdoc />
    public partial class ContactTb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExperienceLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentlyEmployed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkExperience = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CurrentEmployerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentDrawnSalary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoticePeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastEmployerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastDrawnSalary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpectedSalary = table.Column<int>(type: "int", nullable: true),
                    CvFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactTable", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactTable");
        }
    }
}
