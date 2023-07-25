using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class UpdateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_overtimes_tb_m_payslips_payslip_guid",
                table: "tb_m_overtimes");

            migrationBuilder.RenameColumn(
                name: "payslip_guid",
                table: "tb_m_overtimes",
                newName: "PayslipGuid");

            migrationBuilder.RenameIndex(
                name: "IX_tb_m_overtimes_payslip_guid",
                table: "tb_m_overtimes",
                newName: "IX_tb_m_overtimes_PayslipGuid");

            migrationBuilder.AddColumn<DateTime>(
                name: "expired_time",
                table: "tb_m_accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "tb_m_accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_used",
                table: "tb_m_accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "otp",
                table: "tb_m_accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_overtimes_tb_m_payslips_PayslipGuid",
                table: "tb_m_overtimes",
                column: "PayslipGuid",
                principalTable: "tb_m_payslips",
                principalColumn: "guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_overtimes_tb_m_payslips_PayslipGuid",
                table: "tb_m_overtimes");

            migrationBuilder.DropColumn(
                name: "expired_time",
                table: "tb_m_accounts");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "tb_m_accounts");

            migrationBuilder.DropColumn(
                name: "is_used",
                table: "tb_m_accounts");

            migrationBuilder.DropColumn(
                name: "otp",
                table: "tb_m_accounts");

            migrationBuilder.RenameColumn(
                name: "PayslipGuid",
                table: "tb_m_overtimes",
                newName: "payslip_guid");

            migrationBuilder.RenameIndex(
                name: "IX_tb_m_overtimes_PayslipGuid",
                table: "tb_m_overtimes",
                newName: "IX_tb_m_overtimes_payslip_guid");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_overtimes_tb_m_payslips_payslip_guid",
                table: "tb_m_overtimes",
                column: "payslip_guid",
                principalTable: "tb_m_payslips",
                principalColumn: "guid",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
