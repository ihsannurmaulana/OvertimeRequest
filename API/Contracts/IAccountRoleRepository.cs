using API.Models;

namespace API.Contracts;

public interface IAccountRoleRepository : IGeneralRepository<AccountRole>
{
    IEnumerable<AccountRole> GetAccountRolesByAccountGuid(Guid guid);
}
