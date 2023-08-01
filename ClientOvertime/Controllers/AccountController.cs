using ClientOvertime.Contracts;
using ClientOvertime.ViewModels.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace ClientOvertime.Controllers;

public class AccountController : Controller
{
	private readonly IAccountRepository _accountRepository;

	public AccountController(IAccountRepository accountRepository)
	{
		_accountRepository = accountRepository;
	}

	[HttpGet]
	public IActionResult Login()
	{
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Login(AccountVMLogin accountVmLogin)
	{
		var result = await _accountRepository.Login(accountVmLogin);
		if (result is null)
		{
			return RedirectToAction("Error", "Home");
		}
		else if (result.Code == 400)
		{
			ModelState.AddModelError(string.Empty, result.Message);
			return View();
		}
		else if (result.Code == 200)
		{
			HttpContext.Session.SetString("JWTToken", result.Data);
			return RedirectToAction("Index", "Dashboard");
		}
		return View();
	}

	[HttpGet]
	public IActionResult ForgotPassword()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> ForgotPassword(AccountVMForgotPassword accountVmForgotPassword)
	{
		var result = await _accountRepository.ForgotPassword(accountVmForgotPassword);

		if (result is null)
		{
			return RedirectToAction("Error", "Home");
		}
		else if (result.Code == 400)
		{
			ModelState.AddModelError(string.Empty, result.Message);
			TempData["Error"] = $"Something Went Wrong! - {result.Message}!";
			return View();
		}
		else if (result.Code == 200)
		{
			TempData["Success"] = $"Data has been Successfully Registered! - {result.Message}!";
			return View("ChangePassword");
		}

		return View();
	}

	[HttpGet]
	public IActionResult ChangePassword()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> ChangePassword(AccountVMChangePassword accountVmChangePassword)
	{
		var result = await _accountRepository.ChangePassword(accountVmChangePassword);

		if (result is null)
		{
			return RedirectToAction("Error", "Home");
		}
		else if (result.Code == 400)
		{
			ModelState.AddModelError(string.Empty, result.Message);
			TempData["Error"] = $"Something Went Wrong! - {result.Message}!";
			return View();
		}
		else if (result.Code == 200)
		{
			TempData["Success"] = $"Data has been Successfully Registered! - {result.Message}!";
			return View("Login");
		}

		return View();
	}

	public IActionResult Logout()
	{
		HttpContext.Session.Clear();
		return RedirectToAction("Index", "Home");
	}
}
