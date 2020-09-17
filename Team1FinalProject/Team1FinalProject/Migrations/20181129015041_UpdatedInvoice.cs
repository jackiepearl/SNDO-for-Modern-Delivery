using Microsoft.EntityFrameworkCore.Migrations;

namespace Team1FinalProject.Migrations
{
    public partial class UpdatedInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "InvoiceDets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDets_UserId",
                table: "InvoiceDets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDets_AspNetUsers_UserId",
                table: "InvoiceDets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDets_AspNetUsers_UserId",
                table: "InvoiceDets");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceDets_UserId",
                table: "InvoiceDets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "InvoiceDets");
        }
    }
}
