﻿using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Employees;

public class EmployeeDtoUpdate
{
    public Guid Guid { get; set; }
    public string Nik { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public GenderEnum Gender { get; set; }
    public DateTime HiringDate { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public Guid? ManagerGuid { get; set; }
    public Guid RoleGuid { get; set; }


    public static implicit operator Employee(EmployeeDtoUpdate updateemployeeDto)
    {
        return new()
        {
            Guid = updateemployeeDto.Guid,
            Nik = updateemployeeDto.Nik,
            FirstName = updateemployeeDto.FirstName,
            LastName = updateemployeeDto.LastName,
            BirthDate = updateemployeeDto.BirthDate,
            Gender = updateemployeeDto.Gender,
            HiringDate = updateemployeeDto.HiringDate,
            PhoneNumber = updateemployeeDto.PhoneNumber,
            ManagerGuid = updateemployeeDto.ManagerGuid
        };
    }

    public static explicit operator EmployeeDtoUpdate(Employee employee)
    {
        return new()
        {
            Guid = employee.Guid,
            Nik = employee.Nik,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            BirthDate = employee.BirthDate,
            Gender = employee.Gender,
            HiringDate = employee.HiringDate,
            PhoneNumber = employee.PhoneNumber,
            ManagerGuid = employee.ManagerGuid
        };
    }
}
