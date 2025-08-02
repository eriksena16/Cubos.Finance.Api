using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cubos.Finance.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixaccountTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Account",
                schema: "dbo",
                table: "BankAccount",
                type: "character varying(9)",
                unicode: false,
                maxLength: 9,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(8)",
                oldUnicode: false,
                oldMaxLength: 8,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Account",
                schema: "dbo",
                table: "BankAccount",
                type: "character varying(8)",
                unicode: false,
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(9)",
                oldUnicode: false,
                oldMaxLength: 9,
                oldNullable: true);
        }
    }
}
