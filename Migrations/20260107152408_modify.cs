using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildWeek2.Migrations
{
    /// <inheritdoc />
    public partial class modify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prodotti_Fornitori_FornitoreId1",
                table: "Prodotti");

            migrationBuilder.DropIndex(
                name: "IX_Prodotti_FornitoreId1",
                table: "Prodotti");

            migrationBuilder.DropColumn(
                name: "FornitoreId1",
                table: "Prodotti");

            migrationBuilder.AlterColumn<Guid>(
                name: "FornitoreId",
                table: "Prodotti",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Prodotti_FornitoreId",
                table: "Prodotti",
                column: "FornitoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prodotti_Fornitori_FornitoreId",
                table: "Prodotti",
                column: "FornitoreId",
                principalTable: "Fornitori",
                principalColumn: "FornitoreId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prodotti_Fornitori_FornitoreId",
                table: "Prodotti");

            migrationBuilder.DropIndex(
                name: "IX_Prodotti_FornitoreId",
                table: "Prodotti");

            migrationBuilder.AlterColumn<string>(
                name: "FornitoreId",
                table: "Prodotti",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "FornitoreId1",
                table: "Prodotti",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Prodotti_FornitoreId1",
                table: "Prodotti",
                column: "FornitoreId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Prodotti_Fornitori_FornitoreId1",
                table: "Prodotti",
                column: "FornitoreId1",
                principalTable: "Fornitori",
                principalColumn: "FornitoreId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
