using ClientOvertime.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientOvertime.Controllers;

public class DashboardController : Controller
{
    private readonly IPayslipRepository _payslipRepository;
    public DashboardController(IPayslipRepository payslipRepository)
    {
        _payslipRepository = payslipRepository;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var statistic = await _payslipRepository.GetStatistic();
        ViewData["statistic"] = statistic.Data;
        return View();
    }

}

