using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryManagementProject.Migrations
{
    public partial class updateborrowdetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BorrowBookDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BookId = table.Column<Guid>(nullable: false),
                    Qty = table.Column<int>(nullable: false),
                    DateBorrow = table.Column<DateTime>(nullable: false),
                    DateRepay = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowBookDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BorrowBookDetails_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BorrowBookDetails_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BorrowBookDetails_BookId",
                table: "BorrowBookDetails",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowBookDetails_UserId",
                table: "BorrowBookDetails",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BorrowBookDetails");
        }
    }
}
