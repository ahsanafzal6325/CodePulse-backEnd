using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CodePulse.EntityFrameworkCore.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class newAuth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "60cfaf67-78b0-4885-a9f4-0e20f73cef92", "aaa39788-f536-4e27-86da-deba74c448c1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d1c3f8b2-4e5a-4f6b-8c7d-9e0f1a2b3c4d", "aaa39788-f536-4e27-86da-deba74c448c1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60cfaf67-78b0-4885-a9f4-0e20f73cef92");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1c3f8b2-4e5a-4f6b-8c7d-9e0f1a2b3c4d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaa39788-f536-4e27-86da-deba74c448c1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "60cfaf67-78b0-4885-a9f4-0e20f73cef92", "60cfaf67-78b0-4885-a9f4-0e20f73cef92", "READER", "READER" },
                    { "d1c3f8b2-4e5a-4f6b-8c7d-9e0f1a2b3c4d", "d1c3f8b2-4e5a-4f6b-8c7d-9e0f1a2b3c4d", "WRITER", "WRITER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "aaa39788-f536-4e27-86da-deba74c448c1", 0, "67941c9f-c384-47a5-a89d-1ae63ff05fba", "admin@codepulse.com", false, false, null, "ADMIN@CODEPULSE.COM", "ADMIN@CODEPULSE.COM", "AQAAAAEAACcQAAAAE...", null, false, "36cf9436-2093-4d18-8f80-dc77006c005d", false, "admin@codepulse.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "60cfaf67-78b0-4885-a9f4-0e20f73cef92", "aaa39788-f536-4e27-86da-deba74c448c1" },
                    { "d1c3f8b2-4e5a-4f6b-8c7d-9e0f1a2b3c4d", "aaa39788-f536-4e27-86da-deba74c448c1" }
                });
        }
    }
}
