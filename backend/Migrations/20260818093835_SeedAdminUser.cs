using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCms.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Avatar", "CreatedAt", "Dob", "Email", "Gender", "HomeAddress", "Name", "PasswordHash", "Phone", "Role", "Username", "WorkAddress" },
                values: new object[] { 1, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "admin@gmail.com", null, null, null, "$2a$11$0UA5Tjd/FvouL2uqhNCDf.pjVBFHj5lg9AzkNP90ypXUTI7l0DWSu", null, "Admin", "Admin", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
