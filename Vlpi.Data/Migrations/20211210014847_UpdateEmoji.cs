using Microsoft.EntityFrameworkCore.Migrations;

namespace Vlpi.Data.Migrations
{
    public partial class UpdateEmoji : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Emoji",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Testing enabled" },
                    { 15, "Requirements analysis disabled" },
                    { 14, "Requirements analysis enabled" },
                    { 13, "Win" },
                    { 12, "Student" },
                    { 11, "Note" },
                    { 9, "Brain" },
                    { 10, "Eyes" },
                    { 7, "Project management enabled" },
                    { 6, "Design disabled" },
                    { 5, "Design enabled" },
                    { 4, "Development disabled" },
                    { 3, "Development enabled" },
                    { 2, "Testing disabled" },
                    { 8, "Project management disabled" }
                });

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Testing");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Development");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Design");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Project management");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Requirements analysis");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Emoji",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Emoji",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Emoji",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Emoji",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Emoji",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Emoji",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Emoji",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Emoji",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Emoji",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Emoji",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Emoji",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Emoji",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Emoji",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Emoji",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Emoji",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Requirements analysis");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Software design");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Software modelling");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Coding");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Testing");
        }
    }
}
