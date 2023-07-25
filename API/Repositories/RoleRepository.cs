using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class RoleRepository : GeneralRepository<Role>, IRoleRepository
{
    public RoleRepository(OvertimeDbContext context) : base(context) { }

    public Role? GetByName(string name)
    {
        return _context.Set<Role>().FirstOrDefault(r => r.Name == name);
    }
}
