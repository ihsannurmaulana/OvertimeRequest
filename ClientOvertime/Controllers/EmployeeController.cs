using ClientOvertime.Contracts;
using ClientOvertime.ViewModels.AccountRoles;
using ClientOvertime.ViewModels.Employees;
using ClientOvertime.ViewModels.Payslips;
using ClientOvertime.ViewModels.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientOvertime.Controllers;

public class EmployeeController : Controller
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPayslipRepository _payslipRepository;

    public EmployeeController(IEmployeeRepository employeeRepository, IAccountRepository accountRepository, IRoleRepository roleRepository, IAccountRoleRepository accountRoleRepository, IPayslipRepository payslipRepository)
    {
        _employeeRepository = employeeRepository;
        _accountRepository = accountRepository;
        _roleRepository = roleRepository;
        _accountRoleRepository = accountRoleRepository;
        _payslipRepository = payslipRepository;
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
        var result = await _employeeRepository.GetEmployees();
        var listEmployee = new List<EmployeeVM>();
        if (result.Data != null)
        {
            listEmployee = result.Data.ToList();
        }
        return View(listEmployee);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create()
    {
        var result = await _employeeRepository.GetManager();
        var listManagers = new List<ManagerVM>();

        if (result.Data != null)
        {
            listManagers = result.Data.ToList();
        }

        // add to view data
        ViewData["Managers"] = listManagers;
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(EmployeeVM newEmployeeVM)
    {
        var result = await _accountRepository.CreateAccount(newEmployeeVM);
        if (result.Code == 200)
        {
            TempData["Success"] = "Data berhasil masuk";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            if (result.Errors?.Email?.Length > 0)
            {
                TempData["EmailError"] = $"Email: {result.Errors.Email[0]}";
            }

            // Check for PhoneNumber errors
            if (result.Errors?.PhoneNumber?.Length > 0)
            {
                TempData["PhoneError"] = $"Phone Number: {result.Errors.PhoneNumber[0]}";
            }
            return Redirect("~/employee/create");
        }

    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid guid)
    {
        var employee = await _employeeRepository.Get(guid);

        var payslips = await _payslipRepository.Get();
        var payslipDtos = new List<PayslipVMGet>();
        if (payslips.Data is not null) payslipDtos = payslips.Data.ToList();
        var payslipDto = new PayslipVMGet();
        foreach (var payslip in payslipDtos)
        {
            if (payslip.EmployeeGuid == employee.Data.Guid)
            {
                payslipDto = payslip;
                break;
            }
        }

        var updateEmployee = new EmployeeVM();
        if (employee.Data?.Guid is null)
        {
            return View(employee);
        }
        else
        {
            updateEmployee.Guid = employee.Data.Guid;
            updateEmployee.Nik = employee.Data.Nik;
            updateEmployee.FirstName = employee.Data.FirstName;
            updateEmployee.LastName = employee.Data.LastName;
            updateEmployee.Email = employee.Data.Email;
            updateEmployee.BirthDate = employee.Data.BirthDate;
            updateEmployee.HiringDate = employee.Data.HiringDate;
            updateEmployee.PhoneNumber = employee.Data.PhoneNumber;
            updateEmployee.Manager = employee.Data.Manager;
            updateEmployee.Salary = payslipDto.Salary;
            updateEmployee.RoleName = employee.Data.RoleName;
            updateEmployee.ManagerGuid = employee.Data.ManagerGuid;
            updateEmployee.Password = employee.Data.Password;
            updateEmployee.ConfirmPassword = employee.Data.ConfirmPassword;
        }


        var result = await _employeeRepository.GetManager();
        var listManagers = new List<ManagerVM>();

        if (result.Data != null)
        {
            listManagers = result.Data.ToList();
        }

        var resultAccountRole = await _accountRoleRepository.Get();
        var listAccountRole = new List<AccountRoleVM>();

        if (resultAccountRole.Data != null) listAccountRole = resultAccountRole.Data.ToList();

        var resultRole = await _roleRepository.Get();
        var listRoles = new List<RoleVM>();
        if (result.Data != null)
        {
            listRoles = resultRole.Data.ToList();
        }

        var accountRoleEmployee = new AccountRoleVM();
        foreach (var i in listAccountRole)
        {
            if (i.AccountGuid == employee.Data.Guid)
            {
                accountRoleEmployee = i;
            }
        }

        // add to view data
        ViewData["Managers"] = listManagers;
        ViewData["AccountRole"] = accountRoleEmployee;
        ViewData["Roles"] = listRoles;

        return View(updateEmployee);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(EmployeeVM employeeVM)
    {
        var result = await _employeeRepository.Put(employeeVM.Guid, employeeVM);

        var payslips = await _payslipRepository.Get();
        var payslipDtos = new List<PayslipVMGet>();
        if (payslips.Data is not null) payslipDtos = payslips.Data.ToList();
        var payslipDto = new PayslipsViewModelUpdate();
        foreach (var payslip in payslipDtos)
        {
            if (payslip.EmployeeGuid == employeeVM.Guid)
            {
                payslipDto.Guid = payslip.Guid;
                payslipDto.EmployeeGuid = payslip.EmployeeGuid;
                payslipDto.Salary = employeeVM.Salary;
                break;
            }
        }

        payslipDto.Salary = employeeVM.Salary;
        var payslipUpdated = await _payslipRepository.PutSalary(payslipDto.Guid, payslipDto);


        if (result.Code == 200)
        {
            TempData["Success"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
        else if (result.Status == "401")
        {
            TempData["Error"] = result.Message;
            return Redirect("~/Employee/Update");
        }

        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid guid)
    {
        var employee = await _employeeRepository.Delete(guid);
        switch (employee.Code)
        {
            case 200:
                TempData["Success"] = employee.Message;
                return Redirect("~/Employee/Index");
            case 400:
                TempData["Error"] = employee.Message;
                return Redirect("~/Employee/Index");
            default:
                TempData["Error"] = "Failed to delete employee";
                return Redirect("~/Employee/Index");
        }
    }
}
