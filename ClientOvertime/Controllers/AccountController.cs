using ClientOvertime.Contracts;
using ClientOvertime.ViewModels.Accounts;
using Microsoft.AspNetCore.Authorization;
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
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [AllowAnonymous]
    public async Task<IActionResult> Login(AccountVMLogin accountVmLogin)
    {
        var account = await _accountRepository.Login(accountVmLogin);
        switch (account.Code)
        {
            case 200:
                HttpContext.Session.SetString("JWTToken", account.Data);
                return RedirectToAction("Index", "Dashboard");
            case 400:
                TempData["Error"] = account.Message;
                return Redirect("~/Account/Login");
            default:
                TempData["Error"] = account.Message;
                return Redirect("~/Account/Login");
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
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
    [AllowAnonymous]
    public IActionResult ChangePassword()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
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

    [AllowAnonymous]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}
