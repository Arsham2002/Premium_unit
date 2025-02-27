using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PremiumUnit.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Workshop",
                columns: table => new
                {
                    WorkshopCode = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ListSubmissionInterval = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    TypeOfActivity = table.Column<int>(type: "INTEGER", nullable: false),
                    ActivityStartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EmployerName = table.Column<string>(type: "TEXT", nullable: false),
                    EmployerPhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Premium = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workshop", x => x.WorkshopCode);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PenaltyBaseDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    WorkshopCode = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_Workshop_WorkshopCode",
                        column: x => x.WorkshopCode,
                        principalTable: "Workshop",
                        principalColumn: "WorkshopCode");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_WorkshopCode",
                table: "Invoice",
                column: "WorkshopCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "Workshop");
        }
    }
}
