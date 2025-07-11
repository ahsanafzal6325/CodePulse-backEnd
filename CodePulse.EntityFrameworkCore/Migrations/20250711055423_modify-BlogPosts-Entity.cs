using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodePulse.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class modifyBlogPostsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "BlogPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UrlHandle",
                table: "BlogPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "UrlHandle",
                table: "BlogPosts");
        }
    }
}
