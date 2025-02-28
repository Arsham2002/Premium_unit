using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PremiumUnit.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Workshop_WorkshopCode",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_WorkshopCode",
                table: "Invoice");

            migrationBuilder.AlterColumn<int>(
                name: "WorkshopCode",
                table: "Invoice",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkshopCode1",
                table: "Invoice",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_WorkshopCode1",
                table: "Invoice",
                column: "WorkshopCode1");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Workshop_WorkshopCode1",
                table: "Invoice",
                column: "WorkshopCode1",
                principalTable: "Workshop",
                principalColumn: "WorkshopCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Workshop_WorkshopCode1",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_WorkshopCode1",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "WorkshopCode1",
                table: "Invoice");

            migrationBuilder.AlterColumn<int>(
                name: "WorkshopCode",
                table: "Invoice",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_WorkshopCode",
                table: "Invoice",
                column: "WorkshopCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Workshop_WorkshopCode",
                table: "Invoice",
                column: "WorkshopCode",
                principalTable: "Workshop",
                principalColumn: "WorkshopCode");
        }
    }
}
