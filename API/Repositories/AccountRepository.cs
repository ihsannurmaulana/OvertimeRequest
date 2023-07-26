using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class AccountRepository : GeneralRepository<Account>, IAccountRepository
{
    public AccountRepository(OvertimeDbContext context) : base(context) { }

    public bool IsDuplicateValue(string value)
    {
        return _context.Set<Account>()
                       .FirstOrDefault(e => e.Email.Contains(value)) is null;
    }

    public Account? GetEmployeeByEmail(string email)
    {
        return _context.Set<Account>().FirstOrDefault(e => e.Email == email);
    }


}
