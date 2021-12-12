using Microsoft.EntityFrameworkCore.Migrations;

namespace Vlpi.Data.Migrations
{
    public partial class UpdateTests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "Test",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "XIF2Test",
                table: "Test",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "R_11",
                table: "Test",
                column: "AdminId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "R_11",
                table: "Test");

            migrationBuilder.DropIndex(
                name: "XIF2Test",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Test");
        }
    }
}
