using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_accounts")]
public class Account : BaseEntity
{
    [Column("email", TypeName = "nvarchar(100)")]
    public string Email { get; set; }

    [Column("password", TypeName = "nvarchar(255)")]
    public string Password { get; set; }


    // Cardinality
    public Employee? Employee { get; set; }

    public ICollection<AccountRole>? AccountRoles { get; set; }
}
