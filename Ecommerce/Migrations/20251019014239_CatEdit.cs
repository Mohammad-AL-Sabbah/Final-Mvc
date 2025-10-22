using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class CatEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Carousels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Carousels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Image",
                value: "1.png");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Image",
                value: "1.png");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Image",
                value: "1.png");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "Image",
                value: "1.png");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "Image",
                value: "1.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Categories");

            migrationBuilder.InsertData(
                table: "Carousels",
                columns: new[] { "Id", "Description", "Image", "SubTitle", "Title" },
                values: new object[,]
                {
                    { 1, "this is img 1", "https://media.licdn.com/dms/image/v2/D4E12AQESJNJ_UqRsow/article-cover_image-shrink_720_1280/article-cover_image-shrink_720_1280/0/1677238733511?e=2147483647&v=beta&t=0hFQNfMjXEYCkhtL7M081ZlDAULmWm2rRpIBCJxJywU", "everything you need in one place", "كل ما تحتاج إليه في مكان واحد" },
                    { 2, "this is img2", "https://t4.ftcdn.net/jpg/05/12/45/15/360_F_512451551_IMNBwfurCl6Yz2NSo9P3P9lRQJS3Faas.jpg", "عروض مميزة وبداية جديدة لتسوق ممتع", "عام جديد سعيد" }
                });
        }
    }
}
