using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class blogfeaturemig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BlogFeatures_BlogId",
                table: "BlogFeatures");

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "BlogFeatures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BlogFeatures_AppUserId",
                table: "BlogFeatures",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogFeatures_BlogId",
                table: "BlogFeatures",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogFeatures_AspNetUsers_AppUserId",
                table: "BlogFeatures",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogFeatures_AspNetUsers_AppUserId",
                table: "BlogFeatures");

            migrationBuilder.DropIndex(
                name: "IX_BlogFeatures_AppUserId",
                table: "BlogFeatures");

            migrationBuilder.DropIndex(
                name: "IX_BlogFeatures_BlogId",
                table: "BlogFeatures");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "BlogFeatures");

            migrationBuilder.CreateIndex(
                name: "IX_BlogFeatures_BlogId",
                table: "BlogFeatures",
                column: "BlogId",
                unique: true);
        }
    }
}
