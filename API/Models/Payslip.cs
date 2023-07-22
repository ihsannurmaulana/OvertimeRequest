using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_payslips")]
public class Payslip : BaseEntity
{
    [Column("date")]
    public DateTime Date { get; set; }

    [Column("allowance")]
    public double Allowance { get; set; }

    [Column("salary")]
    public double Salary { get; set; }

    [Column("employee_guid")]
    public Guid EmployeeGuid { get; set; }

    [Column("overtime_guid")]
    public Guid OvertimeGuid { get; set; }

    // Cardinality
    public Overtime? Overtimes { get; set; }

    public Employee? Employee { get; set; }
}
