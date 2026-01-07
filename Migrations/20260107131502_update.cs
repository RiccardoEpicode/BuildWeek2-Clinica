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
            migrationBuilder.DropColumn(
                name: "Name",
                table: "RicoveriAnimali");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "RicoveriAnimali",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
