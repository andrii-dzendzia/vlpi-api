using Microsoft.EntityFrameworkCore.Migrations;

namespace Vlpi.Data.Migrations
{
    public partial class UpdateSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 5,
                column: "IsEnabled",
                value: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 5,
                column: "IsEnabled",
                value: true);
        }
    }
}
