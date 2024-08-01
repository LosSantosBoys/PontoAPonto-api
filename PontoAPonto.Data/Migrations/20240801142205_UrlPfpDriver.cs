using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PontoAPonto.Data.Migrations
{
    /// <inheritdoc />
    public partial class UrlPfpDriver : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlProfilePicute",
                table: "Drivers",
                type: "longtext",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlProfilePicute",
                table: "Drivers");
        }
    }
}
