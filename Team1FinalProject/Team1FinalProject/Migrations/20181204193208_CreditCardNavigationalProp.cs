using Microsoft.EntityFrameworkCore.Migrations;

namespace Team1FinalProject.Migrations
{
    public partial class CreditCardNavigationalProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderCardCreditCardID",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderCardCreditCardID",
                table: "Orders",
                column: "OrderCardCreditCardID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_CreditCards_OrderCardCreditCardID",
                table: "Orders",
                column: "OrderCardCreditCardID",
                principalTable: "CreditCards",
                principalColumn: "CreditCardID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_CreditCards_OrderCardCreditCardID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderCardCreditCardID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderCardCreditCardID",
                table: "Orders");
        }
    }
}
