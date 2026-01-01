using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodePulse.EntityFrameworkCore.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class neww : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60cfaf67-78b0-4885-a9f4-0e20f73cef92",
                column: "NormalizedName",
                value: "READER");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1c3f8b2-4e5a-4f6b-8c7d-9e0f1a2b3c4d",
                column: "NormalizedName",
                value: "WRITER");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaa39788-f536-4e27-86da-deba74c448c1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "67941c9f-c384-47a5-a89d-1ae63ff05fba", "AQAAAAEAACcQAAAAE...", "36cf9436-2093-4d18-8f80-dc77006c005d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60cfaf67-78b0-4885-a9f4-0e20f73cef92",
                column: "NormalizedName",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1c3f8b2-4e5a-4f6b-8c7d-9e0f1a2b3c4d",
                column: "NormalizedName",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaa39788-f536-4e27-86da-deba74c448c1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "61753fc3-cc84-4eb9-8ca0-01aa42d0dc82", "Admin12@", "c6f40d37-bd07-49cc-b1f1-c1a337be503d" });
        }
    }
}
