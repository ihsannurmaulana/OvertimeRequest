using ClientOvertime.Contracts;
using ClientOvertime.ViewModels.Roles;

namespace ClientOvertime.Repositories;

public class RoleRepository : GeneralRepository<RoleVM, Guid>, IRoleRepository
{
    public RoleRepository(string request = "roles/") : base(request)
    {
    }
}
