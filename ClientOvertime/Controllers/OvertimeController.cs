using ClientOvertime.Contracts;
using ClientOvertime.ViewModels.Overtimes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientOvertime.Controllers;

public class OvertimeController : Controller
{
    private readonly IOvertimeRepository _repository;
    private readonly IEmployeeRepository _employeeRepository;

    public OvertimeController(IOvertimeRepository repository, IEmployeeRepository employeeRepository)
    {
        _repository = repository;
        _employeeRepository = employeeRepository;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Index()
    {
        var EmployeeGuid = User.Claims.FirstOrDefault(x => x.Type == "Guid")?.Value;
        var guid = Guid.Parse(EmployeeGuid);
        var employee = await _employeeRepository.Get(guid);
        var result = await _repository.Get();
        var listHistory = new List<OvertimeVMRequest>();
        if (result.Data != null)
        {
            listHistory = result.Data.ToList();
        }

        if (User.IsInRole("Admin") || User.IsInRole("Manager"))
        {
            return View(listHistory);
        }

        var newListHistory = new List<OvertimeVMRequest>();
        foreach (var history in listHistory)
        {
            if (history.EmployeeGuid == employee.Data.Guid)
            {
                newListHistory.Add(history);
            }
        }


        return View(newListHistory);
    }


    //public async Task<IActionResult> IndexEmployee(OvertimeVMRequest over)
    //{
    //	var result = await _repository.Get(over.EmployeeGuid);
    //	var listHistory = new List<OvertimeVMRequest>();
    //	if (result.Data != null)
    //	{
    //		listHistory.Add(result.Data);
    //	}
    //	return View(listHistory);
    //}

    [HttpGet]
    [Authorize(Roles = "User")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> Create(OvertimeVMRequest overtimeVMRequest)
    {
        var result = await _repository.Post(overtimeVMRequest);
        if (result.Code == 200)
        {
            TempData["Success"] = "Data berhasil masuk";
            return RedirectToAction("Index", "Overtime");
        }
        else if (result.Status == "400")
        {
            //ModelState.AddModelError(string.Empty, result.Message);
            TempData["Error"] = "Gagal Create";
            return View();
        }
        return RedirectToAction("Create", "Overtime");
    }


    [HttpGet]
    [Authorize(Roles = "User, Admin")]
    public async Task<IActionResult> Detail(Guid guid)
    {
        var result = await _repository.Get(guid);
        var resultOvertime = new OvertimeVMRequest();
        if (result.Data?.Guid is null)
        {
            return View(result);
        }
        else
        {
            resultOvertime.Guid = result.Data.Guid;
            resultOvertime.OvertimeNumber = result.Data.OvertimeNumber;
            resultOvertime.FullName = result.Data.FullName;
            resultOvertime.CreatedDate = result.Data.CreatedDate;
            resultOvertime.StartDate = result.Data.StartDate;
            resultOvertime.EndDate = result.Data.EndDate;
            resultOvertime.Status = result.Data.Status;
            resultOvertime.Paid = result.Data.Paid;
            resultOvertime.Remarks = result.Data.Remarks;
            resultOvertime.Remaining = result.Data.Remaining;
            resultOvertime.EmployeeGuid = result.Data.EmployeeGuid;
        }

        return View(resultOvertime);
    }

    [HttpGet]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> GetByManager()
    {
        var emp = User.Claims.FirstOrDefault(a => a.Type == "Guid")?.Value;
        var guid = Guid.Parse(emp);
        var result = await _repository.GetOverManager(guid);
        var employees = new List<OvertimeVMRequest>();

        if (result.Data is not null)
        {
            employees = result.Data.ToList();
        }
        return View(employees);
    }




}
