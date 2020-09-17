using Microsoft.EntityFrameworkCore.Migrations;

namespace Team1FinalProject.Migrations
{
    public partial class CapitalizeReviewProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id_book",
                table: "Reviews",
                newName: "Id_book");

            migrationBuilder.AlterColumn<string>(
                name: "ReviewContent",
                table: "Reviews",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id_book",
                table: "Reviews",
                newName: "id_book");

            migrationBuilder.AlterColumn<string>(
                name: "ReviewContent",
                table: "Reviews",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);
        }
    }
}
