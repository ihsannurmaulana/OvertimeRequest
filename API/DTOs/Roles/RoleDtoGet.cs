using API.Models;

namespace API.DTOs.Roles;

public class RoleDtoGet
{
    public Guid Guid { get; set; }
    public string Name { get; set; }

    public static implicit operator Role(RoleDtoGet updateRoleDto)
    {
        return new()
        {
            Guid = updateRoleDto.Guid,
            Name = updateRoleDto.Name
        };
    }

    public static explicit operator RoleDtoGet(Role role)
    {
        return new()
        {
            Guid = role.Guid,
            Name = role.Name
        };
    }
}
