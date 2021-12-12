using Microsoft.EntityFrameworkCore.Migrations;

namespace Vlpi.Data.Migrations
{
    public partial class UpdateModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Module");

            migrationBuilder.AddColumn<int>(
                name: "EmojiId",
                table: "Module",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "Module",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmojiId",
                table: "Module");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "Module");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Module",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
