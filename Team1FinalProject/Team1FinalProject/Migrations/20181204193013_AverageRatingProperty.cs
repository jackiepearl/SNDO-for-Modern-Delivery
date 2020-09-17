using Microsoft.EntityFrameworkCore.Migrations;

namespace Team1FinalProject.Migrations
{
    public partial class AverageRatingProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditCard1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreditCard2",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreditCard3",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreditCard1",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreditCard2",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreditCard3",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
