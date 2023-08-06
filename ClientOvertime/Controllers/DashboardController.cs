using ClientOvertime.Contracts;
using ClientOvertime.ViewModels.Overtimes;
using ClientOvertime.ViewModels.Payslips;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientOvertime.Controllers;

public class DashboardController : Controller
{
    private readonly IPayslipRepository _payslipRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IOvertimeRepository _overtimeRepository;

    public DashboardController(IPayslipRepository payslipRepository, IEmployeeRepository employeeRepository, IOvertimeRepository overtimeRepository)
    {
        _payslipRepository = payslipRepository;
        _employeeRepository = employeeRepository;
        _overtimeRepository = overtimeRepository;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var EmployeeGuid = User.Claims.FirstOrDefault(x => x.Type == "Guid")?.Value;
        var guid = Guid.Parse(EmployeeGuid);
        var salary = await _payslipRepository.Get();
        var listSalary = new List<PayslipVMGet>();
        if (salary.Data is not null)
        {
            listSalary = salary.Data.ToList();
        }

        var newListSalary = new PayslipVMGet();
        foreach (var item in listSalary)
        {
            if (item.EmployeeGuid == guid)
            {
                newListSalary.Salary = item.Salary;
            }
        }
        ViewData["salary"] = newListSalary.Salary;
        var overtime = await _overtimeRepository.Get();
        var listRemaining = new List<OvertimeVMRequest>();
        if (overtime.Data is not null)
        {
            listRemaining = overtime.Data.ToList();
        }

        var newListOvertime = new OvertimeVMRequest();
        foreach (var item in listRemaining)
        {
            if (item.EmployeeGuid == guid)
            {
                newListOvertime.Remaining = item.Remaining;
            }
        }
        ViewData["remaining"] = newListOvertime.Remaining;
        var statistic = await _payslipRepository.GetStatistic();
        ViewData["statistic"] = statistic.Data;
        var countEmployee = await _employeeRepository.GetCount();
        ViewData["countEmployee"] = countEmployee.Data;
        var totalOvertime = await _payslipRepository.GetStatistic(guid);
        ViewData["countOvertime"] = totalOvertime.Data;

        var overtimeManager = await _overtimeRepository.Get();
        var listOvertimeManager = new List<OvertimeVMRequest>();
        if (overtimeManager.Data is not null)
        {
            listOvertimeManager = overtimeManager.Data.ToList();
        }

        var countOvertimeGuid = listOvertimeManager.Count(item => item.EmployeeGuid == guid && item.Status == API.Utilities.Enums.StatusLevel.Waiting);

        ViewData["countOvertime"] = countOvertimeGuid;
        return View();
    }
}

