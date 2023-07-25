using API.Models;

namespace API.Contracts;

public interface IRoleRepository : IGeneralRepository<Role>
{
    Role? GetByName(string name);
}

