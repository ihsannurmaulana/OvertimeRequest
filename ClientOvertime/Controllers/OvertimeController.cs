using ClientOvertime.Contracts;
using ClientOvertime.ViewModels.Overtimes;
using Microsoft.AspNetCore.Mvc;

namespace ClientOvertime.Controllers;

public class OvertimeController : Controller
{
	private readonly IOvertimeRepository _repository;

	public OvertimeController(IOvertimeRepository repository)
	{
		_repository = repository;
	}

	[HttpGet]
	public async Task<IActionResult> Index()
	{
		var result = await _repository.Get();
		var listHistory = new List<OvertimeVMRequest>();
		if (result.Data != null)
		{
			listHistory = result.Data.ToList();
		}
		return View(listHistory);
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
			return RedirectToAction("Index", "Employee");
		}
		else if (result.Status == "409")
		{
			ModelState.AddModelError(string.Empty, result.Message);
			return View();
		}
		return RedirectToAction("Index", "Dashboard");
	}



}
