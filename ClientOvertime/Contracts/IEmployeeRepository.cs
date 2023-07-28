using API.Utilities.Handlers;
using ClientOvertime.ViewModels.Employees;

namespace ClientOvertime.Contracts;

public interface IEmployeeRepository : IGeneralRepository<EmployeeVMRegister, Guid>
{
    Task<ResponseHandler<IEnumerable<EmployeeVMRegister>>> GetEmployees();

    Task<ResponseHandler<IEnumerable<ManagerVM>>> GetManager();
}
