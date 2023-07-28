﻿using API.Models;
using ClientOvertime.Contracts;
using ClientOvertime.ViewModels.Employees;
using Microsoft.AspNetCore.Mvc;

namespace ClientOvertime.Controllers;

public class EmployeeController : Controller
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IAccountRepository _accountRepository;

    public EmployeeController(IEmployeeRepository employeeRepository, IAccountRepository accountRepository)
    {
        _employeeRepository = employeeRepository;
        _accountRepository = accountRepository;
    }

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
    public async Task<IActionResult> Create(EmployeeVM newEmployeeVM)
    {
        var result = await _accountRepository.CreateAccount(newEmployeeVM);
        if (result.Code == 200)
        {
            TempData["Success"] = "Data berhasil masuk";
            return RedirectToAction(nameof(Index));
        }
        else if (result.Status == "409")
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid guid)
    {
        var employee = await _employeeRepository.Get(guid);
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

        // add to view data
        ViewData["Managers"] = listManagers;
        return View(updateEmployee);
    }

    [HttpPost]
    public async Task<IActionResult> Update(EmployeeVM employeeVM)
    {
        var result = await _employeeRepository.Put(employeeVM.Guid, employeeVM);
        if (result.Code == 200)

        {
            return RedirectToAction(nameof(Index));
        }
        else if (result.Status == "409")
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid guid)
    {
        var result = await _employeeRepository.Delete(guid);
        var employee = new Employee();
        if (result.Data?.Guid is null)
        {
            return RedirectToAction(nameof(Index));
        }
        else
        {
            employee.Guid = result.Data.Guid;
        }
        return RedirectToAction(nameof(Index));
    }
}
