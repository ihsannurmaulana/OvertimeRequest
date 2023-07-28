using API.Contracts;
using API.DTOs.Employees;
using API.Models;
using API.Utilities.Handlers;

namespace API.Services;

public class EmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly IRoleRepository _roleRepository;

    public EmployeeService(IEmployeeRepository employeeRepository, IAccountRepository accountRepository, IAccountRoleRepository accountRoleRepository, IRoleRepository roleRepository)
    {
        _employeeRepository = employeeRepository;
        _accountRepository = accountRepository;
        _accountRoleRepository = accountRoleRepository;
        _roleRepository = roleRepository;
    }


    public IEnumerable<EmployeeDtoGetAll> GetManager()
    {
        var allEmployee = GetAllMaster();

        var managerEmployees = allEmployee.Where(employee => employee.RoleName == "Manager");

        return managerEmployees;
    }

    // GetAllEmployeeMaster
    public IEnumerable<EmployeeDtoGetAll> GetAllMaster()
    {
        var master = (from employee in _employeeRepository.GetAll()
                      join account in _accountRepository.GetAll() on employee.Guid equals account.Guid
                      join accountRole in _accountRoleRepository.GetAll() on account.Guid equals accountRole.AccountGuid
                      join role in _roleRepository.GetAll() on accountRole.RoleGuid equals role.Guid
                      select new EmployeeDtoGetAll
                      {
                          Guid = employee.Guid,
                          FullName = employee.FirstName + " " + employee.LastName,
                          Nik = employee.Nik,
                          Email = account.Email,
                          BirthDate = employee.BirthDate,
                          PhoneNumber = employee.PhoneNumber,
                          Gender = employee.Gender,
                          HiringDate = employee.HiringDate,
                          ManagerGuid = employee.ManagerGuid,
                          RoleName = role.Name
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
    public IEnumerable<EmployeeDtoGet> GetEmployee()
    {
        var employees = _employeeRepository.GetAll().ToList();
        if (!employees.Any()) return Enumerable.Empty<EmployeeDtoGet>();
        List<EmployeeDtoGet> employeeDtos = new();

        foreach (var employee in employees)
        {
            employeeDtos.Add((EmployeeDtoGet)employee);
        }

        return employeeDtos;
    }

    // GetByGuid
    public EmployeeDtoGet? GetEmployeeByGuid(Guid guid)
    {
        var employee = _employeeRepository.GetByGuid(guid);
        if (employee is null) return null;

        return (EmployeeDtoGet)employee;
    }

    // GetManagerByGuid
    public EmployeeDtoGet? GetManagerByGuid(Guid guid)
    {
        var manager = _employeeRepository.GetManagerByGuid(guid);
        if (manager is null) return null;

        return (EmployeeDtoGet)manager;
    }

    // Create
    public EmployeeDtoGet? CreateEmployee(EmployeeDtoCreate newEmployeeDto)
    {
        Employee employee = newEmployeeDto;
        employee.Nik = GenerateHandler.Nik(_employeeRepository.GetLastEmployeeNik());

        var createdEmployee = _employeeRepository.Create(employee);
        if (createdEmployee is null) return null; // Employee failed to create

        return (EmployeeDtoGet)createdEmployee; //Employee created
    }

    // Update
    public int UpdateEmployee(EmployeeDtoUpdate employeeDto)
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
