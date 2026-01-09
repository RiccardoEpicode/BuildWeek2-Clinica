using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildWeek2.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prodotti_Fornitori_FornitoreId",
                table: "Prodotti");

            migrationBuilder.AlterColumn<Guid>(
                name: "FornitoreId",
                table: "Prodotti",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Prodotti_Fornitori_FornitoreId",
                table: "Prodotti",
                column: "FornitoreId",
                principalTable: "Fornitori",
                principalColumn: "FornitoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prodotti_Fornitori_FornitoreId",
                table: "Prodotti");

            migrationBuilder.AlterColumn<Guid>(
                name: "FornitoreId",
                table: "Prodotti",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Prodotti_Fornitori_FornitoreId",
                table: "Prodotti",
                column: "FornitoreId",
                principalTable: "Fornitori",
                principalColumn: "FornitoreId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
