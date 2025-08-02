using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cubos.Finance.Data.Migrations
{
    /// <inheritdoc />
    public partial class accountTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankAccount",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PeopleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Branch = table.Column<string>(type: "character varying(3)", unicode: false, maxLength: 3, nullable: true),
                    Account = table.Column<string>(type: "character varying(8)", unicode: false, maxLength: 8, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccount_People_PeopleId",
                        column: x => x.PeopleId,
                        principalSchema: "dbo",
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_Account",
                schema: "dbo",
                table: "BankAccount",
                column: "Account",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_PeopleId",
                schema: "dbo",
                table: "BankAccount",
                column: "PeopleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAccount",
                schema: "dbo");
        }
    }
}
