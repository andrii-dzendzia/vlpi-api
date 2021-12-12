using Microsoft.EntityFrameworkCore.Migrations;

namespace Vlpi.Data.Migrations
{
    public partial class RemoveTestDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Test");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
