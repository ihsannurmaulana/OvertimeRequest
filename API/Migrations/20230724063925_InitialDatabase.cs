using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_employees",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nik = table.Column<int>(type: "int", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    birth_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false),
                    hiring_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    manager_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_employees", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_m_employees_tb_m_employees_manager_guid",
                        column: x => x.manager_guid,
                        principalTable: "tb_m_employees",
                        principalColumn: "guid");
                });

            migrationBuilder.CreateTable(
                name: "tb_m_roles",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_roles", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_accounts",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_accounts", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_m_accounts_tb_m_employees_guid",
                        column: x => x.guid,
                        principalTable: "tb_m_employees",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_payslips",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    allowance = table.Column<double>(type: "float", nullable: false),
                    salary = table.Column<double>(type: "float", nullable: false),
                    employee_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_payslips", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_m_payslips_tb_m_employees_employee_guid",
                        column: x => x.employee_guid,
                        principalTable: "tb_m_employees",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "tb_tr_account_roles",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    account_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    role_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_account_roles", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_tr_account_roles_tb_m_accounts_account_guid",
                        column: x => x.account_guid,
                        principalTable: "tb_m_accounts",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_tb_tr_account_roles_tb_m_roles_role_guid",
                        column: x => x.role_guid,
                        principalTable: "tb_m_roles",
                        principalColumn: "guid");
                });

            migrationBuilder.CreateTable(
                name: "tb_m_overtimes",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    overtime_number = table.Column<int>(type: "int", nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    remarks = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    employee_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    payslip_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_overtimes", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_m_overtimes_tb_m_employees_employee_guid",
                        column: x => x.employee_guid,
                        principalTable: "tb_m_employees",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_m_overtimes_tb_m_payslips_payslip_guid",
                        column: x => x.payslip_guid,
                        principalTable: "tb_m_payslips",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "tb_tr_histories",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    overtime_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_histories", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_tr_histories_tb_m_overtimes_overtime_guid",
                        column: x => x.overtime_guid,
                        principalTable: "tb_m_overtimes",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_accounts_email",
                table: "tb_m_accounts",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_employees_manager_guid",
                table: "tb_m_employees",
                column: "manager_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_employees_nik_phone_number",
                table: "tb_m_employees",
                columns: new[] { "nik", "phone_number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_overtimes_employee_guid",
                table: "tb_m_overtimes",
                column: "employee_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_overtimes_overtime_number",
                table: "tb_m_overtimes",
                column: "overtime_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_overtimes_payslip_guid",
                table: "tb_m_overtimes",
                column: "payslip_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_payslips_employee_guid",
                table: "tb_m_payslips",
                column: "employee_guid",
                unique: true,
                filter: "[employee_guid] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_account_roles_account_guid",
                table: "tb_tr_account_roles",
                column: "account_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_account_roles_role_guid",
                table: "tb_tr_account_roles",
                column: "role_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_histories_overtime_guid",
                table: "tb_tr_histories",
                column: "overtime_guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_tr_account_roles");

            migrationBuilder.DropTable(
                name: "tb_tr_histories");

            migrationBuilder.DropTable(
                name: "tb_m_accounts");

            migrationBuilder.DropTable(
                name: "tb_m_roles");

            migrationBuilder.DropTable(
                name: "tb_m_overtimes");

            migrationBuilder.DropTable(
                name: "tb_m_payslips");

            migrationBuilder.DropTable(
                name: "tb_m_employees");
        }
    }
}
