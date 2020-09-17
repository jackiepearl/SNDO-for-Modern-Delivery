using Microsoft.EntityFrameworkCore.Migrations;

namespace Team1FinalProject.Migrations
{
    public partial class CouponFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderCost",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "OrderTotal",
                table: "Orders",
                newName: "OrderSubtotal");

            migrationBuilder.AlterColumn<string>(
                name: "CreditCardNumber",
                table: "CreditCards",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderSubtotal",
                table: "Orders",
                newName: "OrderTotal");

            migrationBuilder.AddColumn<decimal>(
                name: "OrderCost",
                table: "Orders",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "CreditCardNumber",
                table: "CreditCards",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 4);
        }
    }
}
