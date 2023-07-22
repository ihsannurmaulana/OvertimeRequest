using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_tr_histories")]
public class History : BaseEntity
{
    [Column("employee_guid")]
    public Guid EmployeeGuid { get; set; }

    [Column("overtime_guid")]
    public Guid OvertimeGuid { get; set; }
}
