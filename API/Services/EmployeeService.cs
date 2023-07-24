using API.Contracts;
using API.DTOs.Employees;

namespace API.Services;

public class EmployeeService
{
    private readonly IEmployeeRepository employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        this.employeeRepository = employeeRepository;
    }

    public IEnumerable<GetEmployeeDto> GetEmployee()
    {
        var employees = employeeRepository.GetAll();

    }
}
