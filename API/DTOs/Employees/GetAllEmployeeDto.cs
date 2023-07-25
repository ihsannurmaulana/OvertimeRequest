using API.Utilities.Enums;

namespace API.DTOs.Employees;

public class GetAllEmployeeDto
{
    public Guid Guid { get; set; }
    public string Nik { get; set; }
    public string FullName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
    public GenderEnum Gender { get; set; }
    public DateTime HiringDate { get; set; }
    public string PhoneNumber { get; set; }
    public Guid? ManagerGuid { get; set; }
    public string? Manager { get; set; }
}
