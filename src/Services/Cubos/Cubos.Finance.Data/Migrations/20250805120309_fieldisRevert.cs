using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cubos.Finance.Data.Migrations
{
    /// <inheritdoc />
    public partial class fieldisRevert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transaction_Description",
                schema: "dbo",
                table: "Transaction");

            migrationBuilder.AddColumn<bool>(
                name: "IsReverted",
                schema: "dbo",
                table: "Transaction",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReverted",
                schema: "dbo",
                table: "Transaction");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Description",
                schema: "dbo",
                table: "Transaction",
                column: "Description",
                unique: true);
        }
    }
}
