using API.Utilities.Handlers;
using ClientOvertime.ViewModels.Accounts;
using ClientOvertime.ViewModels.Employees;

namespace ClientOvertime.Contracts;

public interface IAccountRepository : IGeneralRepository<AccountVMGet, string>
{
    Task<ResponseHandler<EmployeeVM>> CreateAccount(EmployeeVM entity);

    Task<ResponseHandler<string>> Login(AccountVMLogin accountVMLogin);
}
