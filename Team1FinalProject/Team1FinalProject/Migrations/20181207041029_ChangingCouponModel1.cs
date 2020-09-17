using Microsoft.EntityFrameworkCore.Migrations;

namespace Team1FinalProject.Migrations
{
    public partial class ChangingCouponModel1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Coupons",
                newName: "ActiveStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActiveStatus",
                table: "Coupons",
                newName: "Active");
        }
    }
}
