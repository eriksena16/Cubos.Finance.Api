using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cubos.Finance.Data.Migrations
{
    /// <inheritdoc />
    public partial class transactionsCorrention : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_BankAccount_BankAccountId",
                schema: "dbo",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "AccountId",
                schema: "dbo",
                table: "Transaction");

            migrationBuilder.AlterColumn<Guid>(
                name: "BankAccountId",
                schema: "dbo",
                table: "Transaction",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Description",
                schema: "dbo",
                table: "Transaction",
                column: "Description",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_BankAccount_BankAccountId",
                schema: "dbo",
                table: "Transaction",
                column: "BankAccountId",
                principalSchema: "dbo",
                principalTable: "BankAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_BankAccount_BankAccountId",
                schema: "dbo",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_Description",
                schema: "dbo",
                table: "Transaction");

            migrationBuilder.AlterColumn<Guid>(
                name: "BankAccountId",
                schema: "dbo",
                table: "Transaction",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                schema: "dbo",
                table: "Transaction",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_BankAccount_BankAccountId",
                schema: "dbo",
                table: "Transaction",
                column: "BankAccountId",
                principalSchema: "dbo",
                principalTable: "BankAccount",
                principalColumn: "Id");
        }
    }
}
