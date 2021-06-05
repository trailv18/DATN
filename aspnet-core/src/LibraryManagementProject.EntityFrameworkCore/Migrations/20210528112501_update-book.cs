using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryManagementProject.Migrations
{
    public partial class updatebook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceBorrow",
                table: "Books");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PriceBorrow",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
