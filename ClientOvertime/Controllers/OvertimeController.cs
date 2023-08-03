using ClientOvertime.Contracts;
using ClientOvertime.ViewModels.Overtimes;
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
	public IActionResult Create()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> Create(OvertimeVMRequest overtimeVMRequest)
	{
		var result = await _repository.Post(overtimeVMRequest);
		if (result.Code == 200)
		{
			TempData["Success"] = "Data berhasil masuk";
			return RedirectToAction("Index", "Overtime");
		}
		else if (result.Status == "409")
		{
			ModelState.AddModelError(string.Empty, result.Message);
			return View();
		}
		return RedirectToAction("Index", "Dashboard");
	}

	[HttpGet]
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
