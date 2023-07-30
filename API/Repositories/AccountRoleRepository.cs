using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class AccountRoleRepository : GeneralRepository<AccountRole>, IAccountRoleRepository
{
    public AccountRoleRepository(OvertimeDbContext context) : base(context) { }

    public IEnumerable<AccountRole> GetAccountRolesByAccountGuid(Guid guid)
    {
        return _context.Set<AccountRole>().Where(ar => ar.AccountGuid == guid);
    }

    public AccountRole GetAccountRoles(Guid guid)
    {
        var entity = _context.Set<AccountRole>().FirstOrDefault(x => x.AccountGuid == guid);

        _context.ChangeTracker.Clear();
        return entity;
    }
}
