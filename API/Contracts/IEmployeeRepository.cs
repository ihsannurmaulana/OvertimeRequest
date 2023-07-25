using API.Models;

namespace API.Contracts;

public interface IEmployeeRepository : IGeneralRepository<Employee>
{
    public string? GetLastEmployeeNik();

    bool IsDuplicateValue(string value);
}
