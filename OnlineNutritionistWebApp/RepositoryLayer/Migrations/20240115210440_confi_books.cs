using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class confibooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GetBookss_AspNetUsers_AppUserId",
                table: "GetBookss");

            migrationBuilder.DropForeignKey(
                name: "FK_GetBookss_Bookss_BooksId",
                table: "GetBookss");

            migrationBuilder.AddForeignKey(
                name: "FK_GetBookss_AspNetUsers_AppUserId",
                table: "GetBookss",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GetBookss_Bookss_BooksId",
                table: "GetBookss",
                column: "BooksId",
                principalTable: "Bookss",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GetBookss_AspNetUsers_AppUserId",
                table: "GetBookss");

            migrationBuilder.DropForeignKey(
                name: "FK_GetBookss_Bookss_BooksId",
                table: "GetBookss");

            migrationBuilder.AddForeignKey(
                name: "FK_GetBookss_AspNetUsers_AppUserId",
                table: "GetBookss",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GetBookss_Bookss_BooksId",
                table: "GetBookss",
                column: "BooksId",
                principalTable: "Bookss",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
