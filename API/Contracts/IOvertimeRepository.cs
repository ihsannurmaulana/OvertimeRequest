using API.Models;

namespace API.Contracts;

public interface IOvertimeRepository : IGeneralRepository<Overtime>
{
    public string? GetLastOvertimeNumber();
}
