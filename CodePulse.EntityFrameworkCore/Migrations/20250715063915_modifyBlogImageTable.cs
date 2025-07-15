using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodePulse.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class modifyBlogImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BlogImages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BlogImages");
        }
    }
}
