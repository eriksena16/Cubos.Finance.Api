using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cubos.Finance.Data.Migrations
{
    /// <inheritdoc />
    public partial class removeRefresh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken",
                schema: "dbo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshToken",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PeopleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Revoked = table.Column<bool>(type: "boolean", nullable: false),
                    Token = table.Column<string>(type: "character varying(500)", unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_PeopleId",
                schema: "dbo",
                table: "RefreshToken",
                column: "PeopleId",
                unique: true);
        }
    }
}
