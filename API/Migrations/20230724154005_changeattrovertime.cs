using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class changeattrovertime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "overtime_number",
                table: "tb_m_overtimes",
                type: "nchar(8)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "overtime_number",
                table: "tb_m_overtimes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(8)");
        }
    }
}
