using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Employees;

public class EmployeeDtoCreate
{
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public GenderEnum Gender { get; set; }
    public DateTime HiringDate { get; set; }
    public string PhoneNumber { get; set; }
    public Guid? ManagerGuid { get; set; }

    public static implicit operator Employee(EmployeeDtoCreate newEmployeeDto)
    {
        return new()
        {
            Guid = new Guid(),
            FirstName = newEmployeeDto.FirstName,
            LastName = newEmployeeDto.LastName,
            BirthDate = newEmployeeDto.BirthDate,
            Gender = newEmployeeDto.Gender,
            HiringDate = newEmployeeDto.HiringDate,
            PhoneNumber = newEmployeeDto.PhoneNumber,
            ManagerGuid = newEmployeeDto.ManagerGuid,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
        };
    }

    public static explicit operator EmployeeDtoCreate(Employee employee)
    {
        return new()
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            BirthDate = employee.BirthDate,
            Gender = employee.Gender,
            HiringDate = employee.HiringDate,
            PhoneNumber = employee.PhoneNumber,
            ManagerGuid = employee.ManagerGuid,
        };
    }
}
