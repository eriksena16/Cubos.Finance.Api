using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cubos.Finance.Data.Migrations
{
    /// <inheritdoc />
    public partial class addCards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Card",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BankAccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: true),
                    Number = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: true),
                    Cvv = table.Column<string>(type: "character varying(3)", unicode: false, maxLength: 3, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Card_BankAccount_BankAccountId",
                        column: x => x.BankAccountId,
                        principalSchema: "dbo",
                        principalTable: "BankAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Card_BankAccountId",
                schema: "dbo",
                table: "Card",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Card_Number",
                schema: "dbo",
                table: "Card",
                column: "Number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Card",
                schema: "dbo");
        }
    }
}
