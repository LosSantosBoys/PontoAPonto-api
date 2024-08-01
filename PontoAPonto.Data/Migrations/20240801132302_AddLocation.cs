using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PontoAPonto.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location_City",
                table: "Drivers",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location_Country",
                table: "Drivers",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location_State",
                table: "Drivers",
                type: "longtext",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location_City",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "Location_Country",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "Location_State",
                table: "Drivers");
        }
    }
}
