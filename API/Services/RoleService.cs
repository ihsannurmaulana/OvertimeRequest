using API.Contracts;
using API.DTOs.Roles;

namespace API.Services;

public class RoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public IEnumerable<RoleDtoGet> GetRole()
    {
        var roles = _roleRepository.GetAll().ToList();
        if (!roles.Any()) return Enumerable.Empty<RoleDtoGet>();
        List<RoleDtoGet> roleDtos = new();

        foreach (var role in roles)
        {
            roleDtos.Add((RoleDtoGet)role);
        }

        return roleDtos;
    }

    public RoleDtoGet? GetRoleByGuid(Guid guid)
    {
        var role = _roleRepository.GetByGuid(guid);
        if (role is null) return null; // Role not found

        return (RoleDtoGet)role; // Role found
    }

    public RoleDtoGet? CreateRole(RoleDtoCreate newRoleDto)
    {
        var createdRole = _roleRepository.Create(newRoleDto);
        if (createdRole is null) return null; // Role failed to create

        return (RoleDtoGet)createdRole; // Role created
    }

    public int UpdateRole(RoleDtoUpdate updateRoleDto)
    {
        var getRole = _roleRepository.GetByGuid(updateRoleDto.Guid);

        if (getRole is null) return -1;

        var isUpdated = _roleRepository.Update(updateRoleDto);
        return !isUpdated ? 0 : // Role Failed
            1; // Role Updated
    }

    public int DeleteRole(Guid guid)
    {
        var role = _roleRepository.GetByGuid(guid);

        if (role is null) return -1; // Role not found

        var isDeleted = _roleRepository.Delete(role);
        return !isDeleted ? 0 : // Role Failed to delete
            1; // Role deleted
    }

}
