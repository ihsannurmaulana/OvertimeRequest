using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_overtimes")]
public class Overtime : BaseEntity
{
    [Column("overtime_number")]
    public int OvertimeNumber { get; set; }

    [Column("start_date")]
    public DateTime StartDate { get; set; }

    [Column("end_date")]
    public DateTime EndDate { get; set; }

    [Column("remarks", TypeName = "nvarchar(255)")]
    public string Remarks { get; set; }

    [Column("status")]
    public StatusLevel Status { get; set; }

    [Column("manager_id")]
    public Guid ManagerId { get; set; }

    // Cardinality
    public ICollection<History>? Histories { get; set; }

    public ICollection<Payslip>? Payslips { get; set; }
}
