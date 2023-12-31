﻿using API.Utilities.Handlers;
using ClientOvertime.Contracts;
using ClientOvertime.ViewModels.Accounts;
using ClientOvertime.ViewModels.Employees;
using Newtonsoft.Json;
using System.Text;

namespace ClientOvertime.Repositories;

public class AccountRepository : GeneralRepository<AccountVMGet, string>, IAccountRepository
{
	public AccountRepository(string request = "accounts/") : base(request) { }

	public async Task<ResponseHandler<EmployeeVM>> CreateAccount(EmployeeVM entity)
	{
		ResponseHandler<EmployeeVM> entityVM = null;
		StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
		using (var response = _httpClient.PostAsync(_request + "register", content).Result)
		{
			string apiResponse = await response.Content.ReadAsStringAsync();
			entityVM = JsonConvert.DeserializeObject<ResponseHandler<EmployeeVM>>(apiResponse);
		}
		return entityVM;
	}

	public async Task<ResponseHandler<string>> Login(AccountVMLogin accountVmLogin)
	{
		ResponseHandler<string> entityVM = null;
		StringContent content = new StringContent(JsonConvert.SerializeObject(accountVmLogin), Encoding.UTF8, "application/json");
		using (var response = _httpClient.PostAsync(_request + "Login", content).Result)
		{
			string apiResponse = await response.Content.ReadAsStringAsync();
			entityVM = JsonConvert.DeserializeObject<ResponseHandler<string>>(apiResponse);
		}
		return entityVM;
	}

	public async Task<ResponseHandler<AccountVMForgotPassword>> ForgotPassword(AccountVMForgotPassword accountVmForgotPassword)
	{
		ResponseHandler<AccountVMForgotPassword> entityVM = null!;
		StringContent content = new StringContent(JsonConvert.SerializeObject(accountVmForgotPassword), Encoding.UTF8, "application/json");
		using (var response = _httpClient.PostAsync(_request + "forgot-password" + "?email=" + accountVmForgotPassword.Email, content).Result)
		{
			string apiResponse = await response.Content.ReadAsStringAsync();
			entityVM = JsonConvert.DeserializeObject<ResponseHandler<AccountVMForgotPassword>>(apiResponse);
		}
		return entityVM;
	}

	public async Task<ResponseHandler<AccountVMForgotPassword>> ChangePassword(AccountVMChangePassword accountVmChangePassword)
	{
		ResponseHandler<AccountVMForgotPassword> entityVM = null;
		StringContent content = new StringContent(JsonConvert.SerializeObject(accountVmChangePassword), Encoding.UTF8, "application/json");
		using (var response = _httpClient.PostAsync(_request + "change-password", content).Result)
		{
			string apiResponse = await response.Content.ReadAsStringAsync();
			entityVM = JsonConvert.DeserializeObject<ResponseHandler<AccountVMForgotPassword>>(apiResponse);
		}
		return entityVM;
	}
}
