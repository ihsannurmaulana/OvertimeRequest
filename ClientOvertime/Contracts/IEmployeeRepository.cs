using API.Utilities.Handlers;
using ClientOvertime.ViewModels.Employees;

namespace ClientOvertime.Contracts;

public interface IEmployeeRepository : IGeneralRepository<EmployeeVM, Guid>
{
    Task<ResponseHandler<IEnumerable<EmployeeVM>>> GetEmployees();

    Task<ResponseHandler<IEnumerable<ManagerVM>>> GetManager();
}
