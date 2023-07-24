using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_tr_histories")]
public class History : BaseEntity
{
    [Column("overtime_guid")]
    public Guid OvertimeGuid { get; set; }

    // Cardinality

    public Overtime? Overtime { get; set; }
}
