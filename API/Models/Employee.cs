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

    [Column("manager_guid")]
    public Guid? ManagerGuid { get; set; }


    // Cardinality
    public Employee? Manager { get; set; }
    public ICollection<Employee>? Employees { get; set; } = new List<Employee>();
    public Account? Account { get; set; }

    public Payslip? Payslip { get; set; }

    public ICollection<Overtime>? Overtimes { get; set; }
}
