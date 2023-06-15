using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoteApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class UrlSuffixAndTotalNotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlSuffix",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfNotes",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlSuffix",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "NumberOfNotes",
                table: "AspNetUsers");
        }
    }
}
