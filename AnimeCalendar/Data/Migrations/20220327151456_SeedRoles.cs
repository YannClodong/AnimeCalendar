using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeCalendar.Data.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3b33eda6-43a1-4ccd-8f69-16a4235d85fd", "60793428-3425-48e6-8be7-5eef35508d65", "ANIME_EDITOR", "ANIME_EDITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "68663419-0275-4322-9255-26cd1c116ab4", "487578b6-8439-46c2-80c2-9b0e90a5a94e", "ADMINISTRATOR", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b33eda6-43a1-4ccd-8f69-16a4235d85fd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "68663419-0275-4322-9255-26cd1c116ab4");
        }
    }
}
