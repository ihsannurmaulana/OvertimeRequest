using API.Utilities.Enums;

namespace ClientOvertime.ViewModels.Employees;

public class EmployeeVM
{
    public Guid Guid { get; set; }
    public string Nik { get; set; }
    public string? FullName { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
    public GenderEnum Gender { get; set; }
    public DateTime HiringDate { get; set; }
    public string PhoneNumber { get; set; }
    public Guid? ManagerGuid { get; set; }
    public string? Manager { get; set; }
    public string RoleName { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public Guid RoleGuid { get; set; }
    public int Salary { get; set; }
}
