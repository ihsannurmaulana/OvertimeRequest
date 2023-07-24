using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_overtimes")]
public class Overtime : BaseEntity
{
    [Column("overtime_number", TypeName = "nchar(8)")]
    public string OvertimeNumber { get; set; }

    [Column("start_date")]
    public DateTime StartDate { get; set; }

    [Column("end_date")]
    public DateTime EndDate { get; set; }

    [Column("remarks", TypeName = "nvarchar(255)")]
    public string Remarks { get; set; }

    [Column("status")]
    public StatusLevel Status { get; set; }

    [Column("employee_guid")]
    public Guid EmployeeGuid { get; set; }

    [Column("payslip_guid")]
    public Guid? PayslipGuid { get; set; }


    // Cardinality
    public ICollection<History>? Histories { get; set; }

    public Employee? Employee { get; set; }

    public Payslip? Payslip { get; set; }
}
