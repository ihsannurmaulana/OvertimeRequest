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
        var listEmployee = new List<EmployeeVMRegister>();
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
    public async Task<IActionResult> Create(EmployeeVMRegister newEmployeeVM)
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
}
