using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryManagementProject.Migrations
{
    public partial class updateborrow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "BorrowBookDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "BorrowBookDetails");
        }
    }
}
