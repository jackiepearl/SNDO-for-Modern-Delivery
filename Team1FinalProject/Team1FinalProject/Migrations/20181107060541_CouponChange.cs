using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Team1FinalProject.Migrations
{
    public partial class CouponChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Coupons_CouponID",
                table: "Users");

            migrationBuilder.DropTable(
                name: "CouponUserDets");

            migrationBuilder.DropIndex(
                name: "IX_Users_CouponID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CouponID",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CouponID",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CouponUserDets",
                columns: table => new
                {
                    CouponUserDetID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CouponID = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponUserDets", x => x.CouponUserDetID);
                    table.ForeignKey(
                        name: "FK_CouponUserDets_Coupons_CouponID",
                        column: x => x.CouponID,
                        principalTable: "Coupons",
                        principalColumn: "CouponID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CouponUserDets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CouponID",
                table: "Users",
                column: "CouponID");

            migrationBuilder.CreateIndex(
                name: "IX_CouponUserDets_CouponID",
                table: "CouponUserDets",
                column: "CouponID");

            migrationBuilder.CreateIndex(
                name: "IX_CouponUserDets_UserId",
                table: "CouponUserDets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Coupons_CouponID",
                table: "Users",
                column: "CouponID",
                principalTable: "Coupons",
                principalColumn: "CouponID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
