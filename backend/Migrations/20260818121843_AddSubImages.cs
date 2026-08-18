using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCms.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddSubImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubImage1",
                table: "Productions",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SubImage2",
                table: "Productions",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SubImage3",
                table: "Productions",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$m85wDTjffu2KhkOuVaDOnOwg3uehHTLg9by36Ppm/DxTjHczTGcju");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubImage1",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "SubImage2",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "SubImage3",
                table: "Productions");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$0UA5Tjd/FvouL2uqhNCDf.pjVBFHj5lg9AzkNP90ypXUTI7l0DWSu");
        }
    }
}
