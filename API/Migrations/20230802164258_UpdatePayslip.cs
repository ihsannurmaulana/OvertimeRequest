using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class UpdatePayslip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_payslips_tb_m_employees_employee_guid",
                table: "tb_m_payslips");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_payslips_employee_guid",
                table: "tb_m_payslips");

            migrationBuilder.AlterColumn<Guid>(
                name: "employee_guid",
                table: "tb_m_payslips",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_payslips_employee_guid",
                table: "tb_m_payslips",
                column: "employee_guid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_payslips_tb_m_employees_employee_guid",
                table: "tb_m_payslips",
                column: "employee_guid",
                principalTable: "tb_m_employees",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_payslips_tb_m_employees_employee_guid",
                table: "tb_m_payslips");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_payslips_employee_guid",
                table: "tb_m_payslips");

            migrationBuilder.AlterColumn<Guid>(
                name: "employee_guid",
                table: "tb_m_payslips",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_payslips_employee_guid",
                table: "tb_m_payslips",
                column: "employee_guid",
                unique: true,
                filter: "[employee_guid] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_payslips_tb_m_employees_employee_guid",
                table: "tb_m_payslips",
                column: "employee_guid",
                principalTable: "tb_m_employees",
                principalColumn: "guid");
        }
    }
}
