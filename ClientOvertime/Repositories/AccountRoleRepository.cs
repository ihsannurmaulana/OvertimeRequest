using ClientOvertime.Contracts;
using ClientOvertime.ViewModels.AccountRoles;

namespace ClientOvertime.Repositories;

public class AccountRoleRepository : GeneralRepository<AccountRoleVM, Guid>, IAccountRoleRepository
{
    public AccountRoleRepository(string request = "account-roles/") : base(request)
    {
    }
}
