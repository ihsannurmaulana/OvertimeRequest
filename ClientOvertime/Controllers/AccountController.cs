using API.DTOs.Accounts;
using API.Models;
using ClientOvertime.Contracts;
using ClientOvertime.ViewModels.Accounts;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace ClientOvertime.Controllers;

public class AccountController : Controller
{
    private readonly IAccountRepository _accountRepository;

    public AccountController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(AccountVMLogin accountVMLogin)
    {
        var result = await _accountRepository.Login(accountVMLogin);
        if (result is null)
        {
            return RedirectToAction("Error", "Home");
        }
        else if (result.Status == "409")
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }
        else if (result.Status == "200")
        {
            // Successful login, store JWT token in session
            HttpContext.Session.SetString("JWToken", result.Data);

            // Redirect to the dashboard page
            return RedirectToAction("Index", "Dashboard");
        }
        return View();
    }
}

    /*[HttpPost]
    public async Task<IActionResult> Auth(LoginVM login)
    {
        var jwtToken = await _accountRepository.Auth(login);
        var token = jwtToken.Token;

        var token2 = new JwtSecurityTokenHandler().ReadJwtToken(token);
        var email = token2.Claims.First(c => c.Type == "email").Value;
        if (token == null)
        {

            return Json(Url.Action("Login", "Accounts"));
        }

        HttpContext.Session.SetString("JWToken", token);

        HttpContext.Session.SetString("Email", email);
        
        var a = HttpContext.Session.GetString("Email");
        
        *//*       return Json(Url.Action("DashboardAdmin", "Home"));*//*

        return Json(Url.Action("Dashboard", "Home"));
    }*/