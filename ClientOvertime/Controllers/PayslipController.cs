using ClientOvertime.Contracts;
using ClientOvertime.ViewModels.Payslips;
using Microsoft.AspNetCore.Mvc;

namespace ClientOvertime.Controllers;

public class PayslipController : Controller
{
    private readonly IPayslipRepository _payslipRepository;

    public PayslipController(IPayslipRepository payslipRepository)
    {
        _payslipRepository = payslipRepository;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _payslipRepository.Get();
        var listPayslip = new List<PayslipVMGet>();
        if (result.Data != null)
        {
            listPayslip = result.Data.ToList();
        }
        return View(listPayslip);
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
