using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PontoAPonto.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationAfterWipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CarInfo_Renavam = table.Column<string>(type: "longtext", nullable: true),
                    CarInfo_Plate = table.Column<string>(type: "longtext", nullable: true),
                    CarInfo_FabricationYear = table.Column<string>(type: "longtext", nullable: true),
                    CarInfo_ModelYear = table.Column<string>(type: "longtext", nullable: true),
                    CarInfo_Brand = table.Column<string>(type: "longtext", nullable: true),
                    CarInfo_Model = table.Column<string>(type: "longtext", nullable: true),
                    CarInfo_Version = table.Column<string>(type: "longtext", nullable: true),
                    CarInfo_Type = table.Column<string>(type: "longtext", nullable: true),
                    CarInfo_Color = table.Column<string>(type: "longtext", nullable: true),
                    CarInfo_Category = table.Column<string>(type: "longtext", nullable: true),
                    CarInfo_Location = table.Column<string>(type: "longtext", nullable: true),
                    CarInfo_OwnerCpfCnpj = table.Column<string>(type: "longtext", nullable: true),
                    Approved = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ApprovedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UrlProfilePicute = table.Column<string>(type: "longtext", nullable: true),
                    Location_Country = table.Column<string>(type: "longtext", nullable: true),
                    Location_State = table.Column<string>(type: "longtext", nullable: true),
                    Location_City = table.Column<string>(type: "longtext", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false),
                    Otp_Password = table.Column<int>(type: "int", nullable: false),
                    Otp_Expiracy = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Otp_Attempts = table.Column<int>(type: "int", nullable: false),
                    Otp_IsVerified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsFirstAccess = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "longblob", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "longblob", nullable: false),
                    PasswordResetToken = table.Column<string>(type: "longtext", nullable: true),
                    ResetTokenExpiracy = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Cpf = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Reputation = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false),
                    Otp_Password = table.Column<int>(type: "int", nullable: false),
                    Otp_Expiracy = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Otp_Attempts = table.Column<int>(type: "int", nullable: false),
                    Otp_IsVerified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsFirstAccess = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "longblob", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "longblob", nullable: false),
                    PasswordResetToken = table.Column<string>(type: "longtext", nullable: true),
                    ResetTokenExpiracy = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Cpf = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Reputation = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_Cpf",
                table: "Drivers",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_Email",
                table: "Drivers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_Phone",
                table: "Drivers",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Cpf",
                table: "Users",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Phone",
                table: "Users",
                column: "Phone",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
