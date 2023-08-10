using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManager.DAL.Migrations
{
    /// <inheritdoc />
    public partial class paymentTierIdinTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentTierId",
                table: "Ticket",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_PaymentTierId",
                table: "Ticket",
                column: "PaymentTierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_PaymentTiers_PaymentTierId",
                table: "Ticket",
                column: "PaymentTierId",
                principalSchema: "Event",
                principalTable: "PaymentTiers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_PaymentTiers_PaymentTierId",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_PaymentTierId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "PaymentTierId",
                table: "Ticket");
        }
    }
}
