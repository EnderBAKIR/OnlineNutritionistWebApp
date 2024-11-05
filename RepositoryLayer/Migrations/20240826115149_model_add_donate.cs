using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class modeladddonate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DonateImage",
                table: "Donates");

            migrationBuilder.AddColumn<string>(
                name: "DonatePdfUrl",
                table: "Donates",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DonatePdfUrl",
                table: "Donates");

            migrationBuilder.AddColumn<string>(
                name: "DonateImage",
                table: "Donates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
