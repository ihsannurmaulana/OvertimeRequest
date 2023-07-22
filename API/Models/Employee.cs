using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_employees")]

public class Employee : BaseEntity
{
    [Column("nik")]
    public int Nik { get; set; }

    [Column("first_name", TypeName = "nvarchar(100)")]
    public string FirstName { get; set; }

    [Column("last_name")]
    public string? LastName { get; set; }

    [Column("birth_date")]
    public DateTime BirthDate { get; set; }

    [Column("gender")]
    public GenderEnum Gender { get; set; }

    [Column("hiring_date")]
    public DateTime HiringDate { get; set; }

    [Column("phone_number", TypeName = "nvarchar(20)")]
    public string PhoneNumber { get; set; }

    [Column("manager_id")]
    public int ManagerId { get; set; }


    // Cardinality
    public Account? Account { get; set; }

    public Payslip? Payslip { get; set; }

    public ICollection<History>? Histories { get; set; }
}
