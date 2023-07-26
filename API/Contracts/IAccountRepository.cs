using API.Models;

namespace API.Contracts;

public interface IAccountRepository : IGeneralRepository<Account>
{
    bool IsDuplicateValue(string value);

    Account? GetEmployeeByEmail(string email);

}

