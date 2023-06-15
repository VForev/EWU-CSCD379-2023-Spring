using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoteApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class UrlSuffixIsUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UrlSuffix",
                table: "Notes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_UrlSuffix",
                table: "Notes",
                column: "UrlSuffix",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Notes_UrlSuffix",
                table: "Notes");

            migrationBuilder.AlterColumn<string>(
                name: "UrlSuffix",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
