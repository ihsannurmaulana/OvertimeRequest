using ClientOvertime.Contracts;
using ClientOvertime.ViewModels.Payslips;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientOvertime.Controllers;

public class PayslipController : Controller
{
    private readonly IPayslipRepository _payslipRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public PayslipController(IPayslipRepository payslipRepository, IEmployeeRepository employeeRepository)
    {
        _payslipRepository = payslipRepository;
        _employeeRepository = employeeRepository;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var EmployeeGuid = User.Claims.FirstOrDefault(x => x.Type == "Guid")?.Value;
        var guid = Guid.Parse(EmployeeGuid);
        var employee = await _employeeRepository.Get(guid);
        var result = await _payslipRepository.Get();
        var listPayslip = new List<PayslipVMGet>();
        if (result.Data != null)
        {
            listPayslip = result.Data.ToList();
        }
        if (User.IsInRole("Admin"))
        {
            return View(listPayslip);
        }

        var newListPayslip = new List<PayslipVMGet>();
        foreach (var history in listPayslip)
        {
            if (history.EmployeeGuid == employee.Data.Guid)
            {
                newListPayslip.Add(history);
            }
        }
        return View(newListPayslip);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(PayslipVMGet newpayslipVMCreate)
    {
        var result = await _payslipRepository.Post(newpayslipVMCreate);
        if (result.Code == 200)
        {
            TempData["Success"] = "Data berhasil masuk";
            return RedirectToAction("Index", "Payslip");
        }
        else if (result.Status == "409")
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }
        return RedirectToAction("Index", "Dashboard");
    }
}
