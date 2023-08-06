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
        var account = await _accountRepository.ForgotPassword(accountVmForgotPassword);
        switch (account.Code)
        {
            case 200:
                TempData["Success"] = account.Message;
                return Redirect("~/Account/ChangePassword");
            case 400:
                TempData["Error"] = account.Message;
                return Redirect("~/Account/ForgotPassword");
            default:
                TempData["Error"] = account.Message;
                return Redirect("~/Account/ForgotPassword");
        }
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
        var account = await _accountRepository.ChangePassword(accountVmChangePassword);
        switch (account.Code)
        {
            case 200:
                TempData["Success"] = account.Message;
                return Redirect("~/Account/Login");
            case 400:
                TempData["Error"] = account.Message;
                return Redirect("~/Account/ChangePassword");
            default:
                TempData["Error"] = account.Message;
                return Redirect("~/Account/ChangePassword");
        }
    }

    [AllowAnonymous]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}
