using API.Utilities.Enums;

namespace ClientOvertime.ViewModels.Employees;

public class EmployeeVMCreate
{
    public Guid Guid { get; set; }
    public string Nik { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public GenderEnum Gender { get; set; }
    public DateTime HiringDate { get; set; }
    public string PhoneNumber { get; set; }
    public Guid? ManagerGuid { get; set; }
}
