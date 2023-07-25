using API.Contracts;
using API.DTOs.Employees;
using API.Models;
using API.Utilities.Handlers;

namespace API.Services;

public class EmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IAccountRepository _accountRepository;

    public EmployeeService(IEmployeeRepository employeeRepository, IAccountRepository accountRepository)
    {
        _employeeRepository = employeeRepository;
        _accountRepository = accountRepository;
    }

    // GetAllEmployeeMaster
    public IEnumerable<GetAllEmployeeDto> GetAllMaster()
    {
        var master = (from employee in _employeeRepository.GetAll()
                      join account in _accountRepository.GetAll() on employee.Guid equals account.Guid
                      select new GetAllEmployeeDto
                      {
                          Guid = employee.Guid,
                          FullName = employee.FirstName + " " + employee.LastName,
                          Nik = employee.Nik,
                          Email = account.Email,
                          BirthDate = employee.BirthDate,
                          PhoneNumber = employee.PhoneNumber,
                          Gender = employee.Gender,
                          HiringDate = employee.HiringDate,
                          ManagerGuid = employee.ManagerGuid
                      }).ToList();

        foreach (var getDataEmployee in master)
        {
            if (getDataEmployee.ManagerGuid != Guid.Empty)
            {
                // Cari data manager berdasarkan ManagerGuid
                var manager = master.FirstOrDefault(e => e.Guid == getDataEmployee.ManagerGuid);
                if (manager != null)
                {
                    getDataEmployee.Manager = $"{manager.Nik} - {manager.FullName}";
                }
            }
        }

        return master;
    }


    // GetAll
    public IEnumerable<GetEmployeeDto> GetEmployee()
    {
        var employees = _employeeRepository.GetAll().ToList();
        if (!employees.Any()) return Enumerable.Empty<GetEmployeeDto>();
        List<GetEmployeeDto> employeeDtos = new();

        foreach (var employee in employees)
        {
            employeeDtos.Add((GetEmployeeDto)employee);
        }

        return employeeDtos;
    }

    // GetByGuid
    public GetEmployeeDto? GetEmployeeByGuid(Guid guid)
    {
        var employee = _employeeRepository.GetByGuid(guid);
        if (employee is null) return null;

        return (GetEmployeeDto)employee;
    }

    // Create
    public GetEmployeeDto? CreateEmployee(NewEmployeeDto newEmployeeDto)
    {
        Employee employee = newEmployeeDto;
        employee.Nik = GenerateHandler.Nik(_employeeRepository.GetLastEmployeeNik());

        var createdEmployee = _employeeRepository.Create(employee);
        if (createdEmployee is null) return null; // Employee failed to create

        return (GetEmployeeDto)createdEmployee; //Employee created
    }

    // Update
    public int UpdateEmployee(UpdateEmployeeDto employeeDto)
    {
        var getEmployee = _employeeRepository.GetByGuid(employeeDto.Guid);

        if (getEmployee is null) return -1; // Employee not found

        var isUpdate = _employeeRepository.Update(employeeDto);
        return !isUpdate ? 0 : // Employee failed to update
            1;                 // Employee updated
    }

    // Delete
    public int DeleteEmployee(Guid guid)
    {
        var employee = _employeeRepository.GetByGuid(guid);

        if (employee is null) return -1; // Employee not found

        var isDelete = _employeeRepository.Delete(employee);
        return !isDelete ? 0 : // Employee failed to delete
            1;                 // Employee deleted
    }

}
