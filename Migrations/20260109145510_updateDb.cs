using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildWeek2.Migrations
{
    /// <inheritdoc />
    public partial class updateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RicoveriAnimali_Animali_AnimaleId",
                table: "RicoveriAnimali");

            migrationBuilder.AlterColumn<Guid>(
                name: "AnimaleId",
                table: "RicoveriAnimali",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_RicoveriAnimali_Animali_AnimaleId",
                table: "RicoveriAnimali",
                column: "AnimaleId",
                principalTable: "Animali",
                principalColumn: "AnimaleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RicoveriAnimali_Animali_AnimaleId",
                table: "RicoveriAnimali");

            migrationBuilder.AlterColumn<Guid>(
                name: "AnimaleId",
                table: "RicoveriAnimali",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RicoveriAnimali_Animali_AnimaleId",
                table: "RicoveriAnimali",
                column: "AnimaleId",
                principalTable: "Animali",
                principalColumn: "AnimaleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
