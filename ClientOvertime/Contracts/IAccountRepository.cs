using API.Utilities.Handlers;
using ClientOvertime.ViewModels.Accounts;
using ClientOvertime.ViewModels.Employees;

namespace ClientOvertime.Contracts;

public interface IAccountRepository : IGeneralRepository<AccountVMGet, string>
{
    Task<ResponseHandler<EmployeeVM>> CreateAccount(EmployeeVM entity);
    public Task<ResponseHandler<string>> Login(AccountVMLogin accountVmLogin);
}
