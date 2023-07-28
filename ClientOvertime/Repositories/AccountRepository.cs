using API.Utilities.Handlers;
using ClientOvertime.Contracts;
using ClientOvertime.ViewModels.Accounts;
using ClientOvertime.ViewModels.Employees;
using Newtonsoft.Json;
using System.Text;

namespace ClientOvertime.Repositories;

public class AccountRepository : GeneralRepository<AccountVMGet, string>, IAccountRepository
{
    public AccountRepository(string request = "accounts/") : base(request) { }
    public async Task<ResponseHandler<EmployeeVMRegister>> CreateAccount(EmployeeVMRegister entity)
    {
        ResponseHandler<EmployeeVMRegister> entityVM = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        using (var response = _httpClient.PostAsync(_request + "register", content).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseHandler<EmployeeVMRegister>>(apiResponse);
        }
        return entityVM;
    }
}
